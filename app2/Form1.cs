using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace app2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                "Версия 1.0",
                "О программе! Гром и молния!");
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
                if (radioButtonOhrana.Checked)
                {
                    Ohrana(filePath);
                }
                else
                if (radioButtonProd.Checked)
                {
                    Prodovolka(filePath);
                }
                else
                if (radioButtonVrach.Checked)
                {
                    Vrach(filePath);
                }
            }
            // если не был выбран файл
            else
            {
                MessageBox.Show("Пират, ты не выбрал файл!", "300 якорей мне в зад!");
            }
        }



        // ПОДСЧЕТ ДЕЯТЕЛЬНОСТИ
        public void Ohrana(string filePath)
        {
            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> leaders = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();

            string[] reportLines = File.ReadAllLines(filePath);
            string leadingPlayerId = string.Empty;

            foreach (string line in reportLines)
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


        public void Prodovolka(string Path)
        {

            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();
            Dictionary<string, int> assistants = new Dictionary<string, int>();
            Dictionary<string, Dictionary<string, int>> caughtAnimals = new Dictionary<string, Dictionary<string, int>>();

            string[] reportLines = File.ReadAllLines(filePath);

            string currentDate = string.Empty;
            string[] dateFormats = { "dd.MM.yy", "dd.MM.yyyy" };

            foreach (string line in reportLines)
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

            // Вывод результатов
            textBoxAll.Text += "ПРОДОВОЛЬСТВЕННАЯ СФЕРА\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += ($"собирал {entry.Key} {entry.Value}\r\n");
            }

            foreach (KeyValuePair<string, int> entry in participants)
            {
                textBoxAll.Text += ($"участвовал {entry.Key} {entry.Value}\r\n");
            }

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

        public void Vrach(string Path)
        {
            Dictionary<string, int> singleCollectors = new Dictionary<string, int>();
            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();

            string[] reportLines = File.ReadAllLines(filePath);

            bool isSingleEvent = false;
            string singleEventCollectorId = string.Empty;

            foreach (string line in reportLines)
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