using Newtonsoft.Json;

namespace work_with_forms
{
    public partial class Form1 : Form
    {
        private const string FORM_NAME = "PREDICTOR";
        private readonly string PREDICTION_PATH = $"{Environment.CurrentDirectory}\\Predictions.json";
        private string[] _predictions;
        private Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = FORM_NAME;
            try
            {
                var fileData = File.ReadAllText(PREDICTION_PATH);
                _predictions = JsonConvert.DeserializeObject<string[]>(fileData);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
            finally
            {
                if (_predictions == null)
                    Close();

                if (_predictions.Length == 0)
                {
                    MessageBox.Show("There no predictions annymore!");
                    Close();
                }
            }
        }

        async private void bClick_Click(object sender, EventArgs e)
        {
            bClick.Enabled = false;

            if (textBoxForName.Text.Length == 0)
                MessageBox.Show("What a pity! \nI can't do an operation, because I don't know your name");
            else
            {
                await Task.Run(() =>
                {
                    for (int i = 1; i < 101; i++)
                    {
                        Invoke(() =>
                        {
                            progressBar.Value = i;
                            Text = i.ToString() + "%";
                        });
                        Thread.Sleep(5);
                    }
                });

                MessageBox.Show(_predictions[_random.Next(_predictions.Length)] + textBoxForName.Text);
                textBoxForName.Clear();
                progressBar.Value = 0;
                Text = FORM_NAME;
            }
            bClick.Enabled = true;
        }
    }
}