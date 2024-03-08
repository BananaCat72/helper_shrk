using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace app2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            panel1.BackColor = Color.FromArgb(230, panel1.BackColor);
            panel2.BackColor = Color.FromArgb(230, panel1.BackColor);

        }

        private void аToolStripMenuItemSpravka_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ахой!\n" +
                "Это программа для подсчета деятельности сфер ШРК. Тут все просто!\n" +
                "\n" +
                "1. Для начала выберите нужную сферу (НЕ ЗАБЫВАЙТЕ МЕНЯТЬ ФЛАЖОК ДЛЯ ПРОДОВОЛКИ И ВРАЧЕВАЛКИ!!!).\n" +
                "2. Нажимаете на кнопку 'Выбрать файл' и выбираете файл формата *.txt.\n" +
                "В этом файле вы предварительно копируете все отчеты патрулей, охот, веточников/травников/мховников, " +
                "докпатрулей. Что не указано здесь, убираете из файла.\n" +
                "3. Далее жмете кнопку 'Начать подсчет'. Программа подсчитает ваши отчеты и выведет список тех, кто собирал, вел, " +
                "посещал, кто сколько поймал (на охоте). Будет написано в специальном окошке сбоку.\n" +
                "\n" +
                "В левом окне выводятся основные данные. В правом - докпатрули (если выбрана врачевательная сфера).\n" +
                "\n" +
                "Обязательно проверьте вручную, не упустили ли отписавшие отчеты некоторые данные, потому что программа не укажет на ошибки!\n" +
                "\n" +
                "Если формат отчетов изменится или найдете баги, обратитесь к Мику Ман-Ди [1532143].\n",
                "Справка! Встать на якорь!");
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Разработал данную программу Мик Ман-Ди [1532143].\n" +
                "При создании кода пользовался нейросетью ChatGPT.\n" +
                "\n" +
                "Версия 1.4",
                "О программе! Гром и молния!");
        }


        private void обновлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                using (Process process = new())
                {
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.FileName = "https://github.com/BananaCat72/helper_shrk";
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //все переменные программы
        string filePath;

        // КНОПКИ
        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Устанавливаем фильтр для типов файлов
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            // Показываем диалог открытия файла и ожидаем результата
            DialogResult result = openFileDialog.ShowDialog();

            // Проверяем, был ли файл выбран и нажата кнопка "ОК"
            if (result == DialogResult.OK)
            {
                // Получаем полный путь к выбранному файлу
                filePath = openFileDialog.FileName;

                // Выводим полный путь к файлу (отладка)
                //MessageBox.Show("Выбранный файл: " + filePath);

                toolStripStatusLabelFilePath.Text = filePath;
            }
        }

        private void buttonPodchet_Click(object sender, EventArgs e)
        {

            // проверка на выбор файла
            if (filePath != null && File.Exists(filePath))
            {
                textBoxAll.Clear();
                textBoxDokpat.Clear();
                string[] reportLines = File.ReadAllLines(filePath);
                if (radioButtonOhrana.Checked)
                {
                    Ohrana(filePath, reportLines);
                }
                else
                if (radioButtonProd.Checked)
                {
                    Prodovolka(filePath, reportLines);
                }
                else
                if (radioButtonVrach.Checked)
                {
                    Vrach(filePath, reportLines);
                    Vrach_DokPat(filePath, reportLines);
                }
            }
            // если не был выбран файл
            else if (filePath == null)
            {
                MessageBox.Show("Пират, ты не выбрал файл!", "300 якорей мне в зад!");
            }
            // если файл каким-то чудом удалили или переместили
            else
            {
                MessageBox.Show("Пират, ты куда дел файл с отчетами? Верни его на место!", "Разрази тебя гром!");
            }
        }

        public bool ProverkaNaSpheru(string[] reportLines, bool ohrana, bool prod, bool vrach, string line)
        {
            bool continuePodchet;
            if (line.StartsWith("Отчёт о пограничном патруле") && (ohrana == false))
            {
                continuePodchet = false;
            }
            else if (line.StartsWith("Отчёт об охотничьем патруле") && (prod == false))
            {
                continuePodchet = false;
            }
            else if ((line.StartsWith("Отчёт об одиночном") || line.StartsWith("Отчёт о веточнике") || line.StartsWith("Отчёт о травнике")
                || line.StartsWith("Отчёт о мховнике")) && (vrach == false))
            {
                continuePodchet = false;
            }
            else continuePodchet = true;
            return continuePodchet;
        }

        public bool ProverkaNaLishnee(string[] reportLines, string line)
        {
            bool continuePodchet;
            if (line.StartsWith("Отчёт о рейде"))
            {
                continuePodchet = false;
            }
            else
            {
                continuePodchet = true;
            }

            return continuePodchet;
        }


        // ПОДСЧЕТ ДЕЯТЕЛЬНОСТИ
        public void Ohrana(string filePath, string[] reportLines)
        {

            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> leaders = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();

            string leadingPlayerId = string.Empty;

            foreach (string line in reportLines)
            {
                if (ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line)
                    && ProverkaNaLishnee(reportLines, line))
                {
                    if (line.StartsWith("Собирающий:"))
                    {
                        Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                        if (collectorMatch.Success)
                        {
                            string collectorId = collectorMatch.Groups[1].Value;
                            collectors.TryGetValue(collectorId, out int count);
                            collectors[collectorId] = count + 1;
                        }
                    }
                    else if (line.StartsWith("Ведущий:"))
                    {
                        Match leadingPlayerMatch = Regex.Match(line, @"\[(\d+)\]");
                        if (leadingPlayerMatch.Success)
                        {
                            leadingPlayerId = leadingPlayerMatch.Groups[1].Value;
                        }

                        MatchCollection leaderMatches = Regex.Matches(line, @"\[(\d+)\]");
                        foreach (Match match in leaderMatches)
                        {
                            string leaderId = match.Groups[1].Value;
                            leaders.TryGetValue(leaderId, out int count);
                            leaders[leaderId] = count + 1;
                        }
                    }
                    else if ((line.StartsWith("Участники:") || line.StartsWith("Участник(и):") || line.StartsWith("Участник:")))
                    {
                        MatchCollection participantMatches = Regex.Matches(line, @"\[(\d+)\]");
                        foreach (Match match in participantMatches)
                        {
                            string participantId = match.Groups[1].Value;
                            if (participantId != leadingPlayerId)
                            {
                                participants.TryGetValue(participantId, out int count);
                                participants[participantId] = count + 1;
                            }
                        }
                    }
                }
                else if (!ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line))
                {
                    MessageBox.Show("Пират, в этом документе есть отчеты из другой сферы!", "Кальмарьи кишки!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("Пират, тут есть что-то лишнее, и оно помешает подсчету!", "Тысяча горбатых моллюсков!");
                    break;
                }
            }

            // Вывод результатов
            textBoxAll.Text += "ОХРАННАЯ СФЕРА\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += $"собирал {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in leaders)
            {
                textBoxAll.Text += $"вел {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in participants)
            {
                textBoxAll.Text += $"участвовал {entry.Key} {entry.Value}\r\n";
            }
        }

        public void Prodovolka(string Path, string[] reportLines)
        {

            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();
            Dictionary<string, int> assistants = new Dictionary<string, int>();
            Dictionary<string, Dictionary<string, int>> caughtAnimals = new Dictionary<string, Dictionary<string, int>>();

            string currentDate = string.Empty;
            string[] dateFormats = { "dd.MM.yy", "dd.MM.yyyy" };

            foreach (string line in reportLines)
            {
                if ((ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line)
                    && ProverkaNaLishnee(reportLines, line)))
                {
                    Match dateMatch = Regex.Match(line, @"\d{2}\.\d{2}\.\d{2,4}");
                    if (dateMatch.Success)
                    {
                        string dateStr = dateMatch.Value;
                        DateTime date;
                        if (DateTime.TryParseExact(dateStr, dateFormats, null, System.Globalization.DateTimeStyles.None, out date))
                        {
                            currentDate = date.ToString("yyyy-MM-dd");
                        }
                    }
                    else if (line.StartsWith("Собирающий:"))
                    {
                        Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                        if (collectorMatch.Success)
                        {
                            string collectorId = collectorMatch.Groups[1].Value;
                            collectors.TryGetValue(collectorId, out int count);
                            collectors[collectorId] = count + 1;
                        }
                    }
                    else if (line.StartsWith("Участники:"))
                    {
                        MatchCollection participantMatches = Regex.Matches(line, @"(.+?)\s\[(\d+)\]\s\(\+(\d+)\)");
                        foreach (Match match in participantMatches)
                        {
                            string participantName = match.Groups[1].Value;
                            string participantId = match.Groups[2].Value;
                            int participantCount = int.Parse(match.Groups[3].Value);

                            participants.TryGetValue(participantId, out int count);
                            participants[participantId] = count + participantCount;

                            if (!caughtAnimals.ContainsKey(participantId))
                            {
                                caughtAnimals[participantId] = new Dictionary<string, int>();
                            }
                            if (!caughtAnimals[participantId].ContainsKey(currentDate))
                            {
                                caughtAnimals[participantId][currentDate] = 0;
                            }
                            caughtAnimals[participantId][currentDate] += participantCount;
                        }
                    }
                    else if (line.StartsWith("Помощники:"))
                    {
                        MatchCollection assistantMatches = Regex.Matches(line, @"(.+?)\s\[(\d+)\]");
                        foreach (Match match in assistantMatches)
                        {
                            string assistantName = match.Groups[1].Value;
                            string assistantId = match.Groups[2].Value;

                            assistants.TryGetValue(assistantId, out int count);
                            assistants[assistantId] = count + 1;
                        }
                    }
                }
                if (!ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line))
                {
                    MessageBox.Show("Пират, в этом документе есть отчеты из другой сферы!", "Кальмарьи кишки!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("Пират, тут есть что-то лишнее, и оно будет мешает подсчету!", "Тысяча горбатых моллюсков!");
                    break;
                }
            }

            // Вывод результатов
            textBoxAll.Text += "ПРОДОВОЛЬСТВЕННАЯ СФЕРА\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += ($"собирал {entry.Key} {entry.Value}\r\n");
            }

            //foreach (KeyValuePair<string, int> entry in participants)
            //{
            //    textBoxAll.Text += ($"участвовал {entry.Key} {entry.Value}\r\n");
            //}

            foreach (KeyValuePair<string, int> entry in assistants)
            {
                textBoxAll.Text += ($"помогал {entry.Key} {entry.Value}\r\n");
            }

            foreach (KeyValuePair<string, Dictionary<string, int>> entry in caughtAnimals)
            {
                textBoxAll.Text += ($"{entry.Key}:\r\n");

                foreach (KeyValuePair<string, int> animals in entry.Value)
                {
                    textBoxAll.Text += ($"{animals.Key}, поймал {animals.Value}\r\n");
                }
            }
        }

        public void Vrach_DokPat(string Path, string[] reportLines)
        {
            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, Dictionary<string, int>> participants = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, int> assistants = new Dictionary<string, int>();

            bool isDoctorPatrol = false;
            foreach (string line in reportLines)
            {
                if ((ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line)
                    && ProverkaNaLishnee(reportLines, line)))
                {
                    if (line.StartsWith("Отчёт о докторском патруле"))
                    {
                        isDoctorPatrol = true;
                    }
                    else if (line.StartsWith("Отчёт о веточнике") || line.StartsWith("Отчёт о травнике") || line.StartsWith("Отчёт о мховнике") || line.StartsWith("Отчёт об одиночном"))
                    {
                        isDoctorPatrol = false;
                    }

                    if (isDoctorPatrol)
                    {
                        if (line.StartsWith("Участники:"))
                        {
                            MatchCollection participantMatches = Regex.Matches(line, @"(.+?)\s\[(\d+)\]\s\(\+([\d\w\s]+)\)");
                            foreach (Match match in participantMatches)
                            {
                                string participantName = match.Groups[1].Value;
                                string participantId = match.Groups[2].Value;
                                string participantCountString = match.Groups[3].Value;

                                int participantCount = GetParticipantCount(participantCountString);

                                if (!participants.ContainsKey(participantId))
                                {
                                    participants[participantId] = new Dictionary<string, int>();
                                }

                                participants[participantId].TryGetValue("мыши", out int count);
                                participants[participantId]["мыши"] = count + participantCount;
                            }
                        }
                        else if (line.StartsWith("Собирающий:"))
                        {
                            Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                            if (collectorMatch.Success)
                            {
                                string collectorId = collectorMatch.Groups[1].Value;
                                collectors.TryGetValue(collectorId, out int count);
                                collectors[collectorId] = count + 1;
                            }
                        }
                        else if (line.StartsWith("Помощники:"))
                        {
                            MatchCollection assistantMatches = Regex.Matches(line, @"\[(\d+)\]");
                            foreach (Match match in assistantMatches)
                            {
                                string assistantId = match.Groups[1].Value;
                                assistants.TryGetValue(assistantId, out int count);
                                assistants[assistantId] = count + 1;
                            }
                        }
                    }
                }
                if (!ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line))
                {
                    MessageBox.Show("Пират, в этом документе есть отчеты из другой сферы!", "Кальмарьи кишки!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("Пират, тут есть что-то лишнее, и оно будет мешает подсчету!", "Тысяча горбатых моллюсков!");
                    break;
                }

            }
            static int GetParticipantCount(string participantCountString)
            {
                int participantCount = 0;
                MatchCollection countMatches = Regex.Matches(participantCountString, @"\d+");
                foreach (Match match in countMatches)
                {
                    participantCount += int.Parse(match.Value);
                }
                return participantCount;
            }

            // Вывод результатов
            textBoxDokpat.Text += "ДОКПАТРУЛИ\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxDokpat.Text += $"собирал {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, Dictionary<string, int>> entry in participants)
            {
                textBoxDokpat.Text += $"{entry.Key}:\r\n";
                foreach (KeyValuePair<string, int> animalEntry in entry.Value)
                {
                    textBoxDokpat.Text += $"поймано {animalEntry.Value} {animalEntry.Key}\r\n";
                }
            }
            foreach (KeyValuePair<string, int> entry in assistants)
            {
                textBoxDokpat.Text += $"помогал {entry.Key} {entry.Value}\r\n";
            }
        }

        public void Vrach(string Path, string[] reportLines)
        {
            Dictionary<string, int> singleCollectors = new Dictionary<string, int>();
            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();

            bool isSingleEvent = false;
            string singleEventCollectorId = string.Empty;

            bool isDoctorPatrol = false;
            foreach (string line in reportLines)
            {
                if ((ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line)
                    && ProverkaNaLishnee(reportLines, line)))
                {
                    if (line.StartsWith("Отчёт о докторском патруле."))
                    {
                        isDoctorPatrol = true;
                    }
                    else if (line.StartsWith("Отчёт о веточнике") || line.StartsWith("Отчёт о травнике") || line.StartsWith("Отчёт о мховнике")
                        || line.StartsWith("Отчёт об одиночном"))
                    {
                        isDoctorPatrol = false;
                    }

                    if (!isDoctorPatrol)
                    {

                        if (line.StartsWith("Отчёт об одиночном"))
                        {
                            isSingleEvent = true;
                        }
                        else if (line.StartsWith("Отчёт о веточнике.") || line.StartsWith("Отчёт о травнике.") || line.StartsWith("Отчёт о мховнике."))
                        {
                            isSingleEvent = false;
                        }
                        else if (line.StartsWith("Собирающий:") && !isSingleEvent)
                        {
                            Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                            if (collectorMatch.Success)
                            {
                                string collectorId = collectorMatch.Groups[1].Value;
                                collectors.TryGetValue(collectorId, out int count);
                                collectors[collectorId] = count + 1;
                            }
                        }
                        else if (line.StartsWith("Участники:") || line.StartsWith("Участник(и):") || line.StartsWith("Участник:"))
                        {
                            MatchCollection participantMatches = Regex.Matches(line, @"\[(\d+)\]");
                            foreach (Match match in participantMatches)
                            {
                                string participantId = match.Groups[1].Value;
                                if (isSingleEvent)
                                {
                                    singleCollectors.TryGetValue(participantId, out int count);
                                    singleCollectors[participantId] = count + 1;
                                }
                                else
                                {
                                    participants.TryGetValue(participantId, out int count);
                                    participants[participantId] = count + 1;
                                }
                            }
                        }
                    }
                }
                if (!ProverkaNaSpheru(reportLines, radioButtonOhrana.Checked, radioButtonProd.Checked, radioButtonVrach.Checked, line))
                {
                    MessageBox.Show("Пират, в этом документе есть отчеты из другой сферы!", "Кальмарьи кишки!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("Пират, тут есть что-то лишнее, и оно будет мешает подсчету!", "Тысяча горбатых моллюсков!");
                    break;
                }
            }


            // Вывод результатов
            textBoxAll.Text += "ВРАЧЕВАТЕЛЬНАЯ СФЕРА\r\n";

            foreach (KeyValuePair<string, int> entry in singleCollectors)
            {
                textBoxAll.Text += $"одиночный {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += $"собирал {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in participants)
            {
                textBoxAll.Text += $"участвовал {entry.Key} {entry.Value}\r\n";
            }


        }


    }
}