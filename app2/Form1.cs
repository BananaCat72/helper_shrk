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

        private void �ToolStripMenuItemSpravka_Click(object sender, EventArgs e)
        {
            MessageBox.Show("����!\n" +
                "��� ��������� ��� �������� ������������ ���� ���. ��� ��� ������!\n" +
                "\n" +
                "1. ��� ������ �������� ������ ����� (�� ��������� ������ ������ ��� ���������� � ����������!!!).\n" +
                "2. ��������� �� ������ '������� ����' � ��������� ���� ������� *.txt.\n" +
                "� ���� ����� �� �������������� ��������� ��� ������ ��������, ����, ����������/���������/���������, " +
                "�����������. ��� �� ������� �����, �������� �� �����.\n" +
                "3. ����� ����� ������ '������ �������'. ��������� ���������� ���� ������ � ������� ������ ���, ��� �������, ���, " +
                "�������, ��� ������� ������ (�� �����). ����� �������� � ����������� ������ �����.\n" +
                "\n" +
                "� ����� ���� ��������� �������� ������. � ������ - ���������� (���� ������� �������������� �����).\n" +
                "\n" +
                "����������� ��������� �������, �� �������� �� ���������� ������ ��������� ������, ������ ��� ��������� �� ������ �� ������!\n" +
                "\n" +
                "���� ������ ������� ��������� ��� ������� ����, ���������� � ���� ���-�� [1532143].\n",
                "�������! ������ �� �����!");
        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "���������� ������ ��������� ��� ���-�� [1532143].\n" +
                "��� �������� ���� ����������� ���������� ChatGPT.\n" +
                "\n" +
                "������ 1.4",
                "� ���������! ���� � ������!");
        }


        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
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


        //��� ���������� ���������
        string filePath;

        // ������
        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // ������������� ������ ��� ����� ������
            openFileDialog.Filter = "��������� ����� (*.txt)|*.txt|��� ����� (*.*)|*.*";

            // ���������� ������ �������� ����� � ������� ����������
            DialogResult result = openFileDialog.ShowDialog();

            // ���������, ��� �� ���� ������ � ������ ������ "��"
            if (result == DialogResult.OK)
            {
                // �������� ������ ���� � ���������� �����
                filePath = openFileDialog.FileName;

                // ������� ������ ���� � ����� (�������)
                //MessageBox.Show("��������� ����: " + filePath);

                toolStripStatusLabelFilePath.Text = filePath;
            }
        }

        private void buttonPodchet_Click(object sender, EventArgs e)
        {

            // �������� �� ����� �����
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
            // ���� �� ��� ������ ����
            else if (filePath == null)
            {
                MessageBox.Show("�����, �� �� ������ ����!", "300 ������ ��� � ���!");
            }
            // ���� ���� �����-�� ����� ������� ��� �����������
            else
            {
                MessageBox.Show("�����, �� ���� ��� ���� � ��������? ����� ��� �� �����!", "������� ���� ����!");
            }
        }

        public bool ProverkaNaSpheru(string[] reportLines, bool ohrana, bool prod, bool vrach, string line)
        {
            bool continuePodchet;
            if (line.StartsWith("����� � ����������� �������") && (ohrana == false))
            {
                continuePodchet = false;
            }
            else if (line.StartsWith("����� �� ���������� �������") && (prod == false))
            {
                continuePodchet = false;
            }
            else if ((line.StartsWith("����� �� ���������") || line.StartsWith("����� � ���������") || line.StartsWith("����� � ��������")
                || line.StartsWith("����� � ��������")) && (vrach == false))
            {
                continuePodchet = false;
            }
            else continuePodchet = true;
            return continuePodchet;
        }

        public bool ProverkaNaLishnee(string[] reportLines, string line)
        {
            bool continuePodchet;
            if (line.StartsWith("����� � �����"))
            {
                continuePodchet = false;
            }
            else
            {
                continuePodchet = true;
            }

            return continuePodchet;
        }


        // ������� ������������
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
                    if (line.StartsWith("����������:"))
                    {
                        Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                        if (collectorMatch.Success)
                        {
                            string collectorId = collectorMatch.Groups[1].Value;
                            collectors.TryGetValue(collectorId, out int count);
                            collectors[collectorId] = count + 1;
                        }
                    }
                    else if (line.StartsWith("�������:"))
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
                    else if ((line.StartsWith("���������:") || line.StartsWith("��������(�):") || line.StartsWith("��������:")))
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
                    MessageBox.Show("�����, � ���� ��������� ���� ������ �� ������ �����!", "��������� �����!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("�����, ��� ���� ���-�� ������, � ��� �������� ��������!", "������ �������� ���������!");
                    break;
                }
            }

            // ����� �����������
            textBoxAll.Text += "�������� �����\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += $"������� {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in leaders)
            {
                textBoxAll.Text += $"��� {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in participants)
            {
                textBoxAll.Text += $"���������� {entry.Key} {entry.Value}\r\n";
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
                    else if (line.StartsWith("����������:"))
                    {
                        Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                        if (collectorMatch.Success)
                        {
                            string collectorId = collectorMatch.Groups[1].Value;
                            collectors.TryGetValue(collectorId, out int count);
                            collectors[collectorId] = count + 1;
                        }
                    }
                    else if (line.StartsWith("���������:"))
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
                    else if (line.StartsWith("���������:"))
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
                    MessageBox.Show("�����, � ���� ��������� ���� ������ �� ������ �����!", "��������� �����!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("�����, ��� ���� ���-�� ������, � ��� ����� ������ ��������!", "������ �������� ���������!");
                    break;
                }
            }

            // ����� �����������
            textBoxAll.Text += "����������������� �����\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += ($"������� {entry.Key} {entry.Value}\r\n");
            }

            //foreach (KeyValuePair<string, int> entry in participants)
            //{
            //    textBoxAll.Text += ($"���������� {entry.Key} {entry.Value}\r\n");
            //}

            foreach (KeyValuePair<string, int> entry in assistants)
            {
                textBoxAll.Text += ($"������� {entry.Key} {entry.Value}\r\n");
            }

            foreach (KeyValuePair<string, Dictionary<string, int>> entry in caughtAnimals)
            {
                textBoxAll.Text += ($"{entry.Key}:\r\n");

                foreach (KeyValuePair<string, int> animals in entry.Value)
                {
                    textBoxAll.Text += ($"{animals.Key}, ������ {animals.Value}\r\n");
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
                    if (line.StartsWith("����� � ���������� �������"))
                    {
                        isDoctorPatrol = true;
                    }
                    else if (line.StartsWith("����� � ���������") || line.StartsWith("����� � ��������") || line.StartsWith("����� � ��������") || line.StartsWith("����� �� ���������"))
                    {
                        isDoctorPatrol = false;
                    }

                    if (isDoctorPatrol)
                    {
                        if (line.StartsWith("���������:"))
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

                                participants[participantId].TryGetValue("����", out int count);
                                participants[participantId]["����"] = count + participantCount;
                            }
                        }
                        else if (line.StartsWith("����������:"))
                        {
                            Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                            if (collectorMatch.Success)
                            {
                                string collectorId = collectorMatch.Groups[1].Value;
                                collectors.TryGetValue(collectorId, out int count);
                                collectors[collectorId] = count + 1;
                            }
                        }
                        else if (line.StartsWith("���������:"))
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
                    MessageBox.Show("�����, � ���� ��������� ���� ������ �� ������ �����!", "��������� �����!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("�����, ��� ���� ���-�� ������, � ��� ����� ������ ��������!", "������ �������� ���������!");
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

            // ����� �����������
            textBoxDokpat.Text += "����������\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxDokpat.Text += $"������� {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, Dictionary<string, int>> entry in participants)
            {
                textBoxDokpat.Text += $"{entry.Key}:\r\n";
                foreach (KeyValuePair<string, int> animalEntry in entry.Value)
                {
                    textBoxDokpat.Text += $"������� {animalEntry.Value} {animalEntry.Key}\r\n";
                }
            }
            foreach (KeyValuePair<string, int> entry in assistants)
            {
                textBoxDokpat.Text += $"������� {entry.Key} {entry.Value}\r\n";
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
                    if (line.StartsWith("����� � ���������� �������."))
                    {
                        isDoctorPatrol = true;
                    }
                    else if (line.StartsWith("����� � ���������") || line.StartsWith("����� � ��������") || line.StartsWith("����� � ��������")
                        || line.StartsWith("����� �� ���������"))
                    {
                        isDoctorPatrol = false;
                    }

                    if (!isDoctorPatrol)
                    {

                        if (line.StartsWith("����� �� ���������"))
                        {
                            isSingleEvent = true;
                        }
                        else if (line.StartsWith("����� � ���������.") || line.StartsWith("����� � ��������.") || line.StartsWith("����� � ��������."))
                        {
                            isSingleEvent = false;
                        }
                        else if (line.StartsWith("����������:") && !isSingleEvent)
                        {
                            Match collectorMatch = Regex.Match(line, @"\[(\d+)\]");
                            if (collectorMatch.Success)
                            {
                                string collectorId = collectorMatch.Groups[1].Value;
                                collectors.TryGetValue(collectorId, out int count);
                                collectors[collectorId] = count + 1;
                            }
                        }
                        else if (line.StartsWith("���������:") || line.StartsWith("��������(�):") || line.StartsWith("��������:"))
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
                    MessageBox.Show("�����, � ���� ��������� ���� ������ �� ������ �����!", "��������� �����!");
                    break;
                }
                else if (!ProverkaNaLishnee(reportLines, line))
                {
                    MessageBox.Show("�����, ��� ���� ���-�� ������, � ��� ����� ������ ��������!", "������ �������� ���������!");
                    break;
                }
            }


            // ����� �����������
            textBoxAll.Text += "�������������� �����\r\n";

            foreach (KeyValuePair<string, int> entry in singleCollectors)
            {
                textBoxAll.Text += $"��������� {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += $"������� {entry.Key} {entry.Value}\r\n";
            }

            foreach (KeyValuePair<string, int> entry in participants)
            {
                textBoxAll.Text += $"���������� {entry.Key} {entry.Value}\r\n";
            }


        }


    }
}