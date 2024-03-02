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
                "������ 1.0",
                "� ���������! ���� � ������!");
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
            // ���� �� ��� ������ ����
            else
            {
                MessageBox.Show("�����, �� �� ������ ����!", "300 ������ ��� � ���!");
            }
        }



        // ������� ������������
        public void Ohrana(string filePath)
        {
            Dictionary<string, int> collectors = new Dictionary<string, int>();
            Dictionary<string, int> leaders = new Dictionary<string, int>();
            Dictionary<string, int> participants = new Dictionary<string, int>();

            string[] reportLines = File.ReadAllLines(filePath);
            string leadingPlayerId = string.Empty;

            foreach (string line in reportLines)
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

            // ����� �����������
            textBoxAll.Text += "����������������� �����\r\n";
            foreach (KeyValuePair<string, int> entry in collectors)
            {
                textBoxAll.Text += ($"������� {entry.Key} {entry.Value}\r\n");
            }

            foreach (KeyValuePair<string, int> entry in participants)
            {
                textBoxAll.Text += ($"���������� {entry.Key} {entry.Value}\r\n");
            }

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