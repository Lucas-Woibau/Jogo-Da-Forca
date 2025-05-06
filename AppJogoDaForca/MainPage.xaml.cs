using AppJogoDaForca.Libraries.Text;
using AppJogoDaForca.Models;
using AppJogoDaForca.Repositories;

namespace AppJogoDaForca
{
    public partial class MainPage : ContentPage
    {
        private Word _word;
        private int _errors = 0;
        public MainPage()
        {
            InitializeComponent();

            ResetScreen();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            string letter = button.Text;

            var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                ErrorHandler(button);
                await IsGameOver();
                return;
            }

            ReplaceLetter(letter, positions);
            button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Success"] as Style;

            HasWinner();

        }

        #region Handler Success
        private async Task HasWinner()
        {
            if (!LblText.Text.Contains("_"))
            {
                await DisplayAlert("Ganhou!", "Você ganhou o jogo, quer começar um novo jogo?", "Novo Jogo");
                ResetScreen();
            }
        }

        private void ReplaceLetter(string letter, List<int> positions)
        {
            foreach (var position in positions)
            {
                LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
            }
        }

        #endregion

        #region Hendler Error
        private void ErrorHandler(Button button)
        {
            _errors++;
            ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
            button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Fail"] as Style;
        }

        private async Task IsGameOver()
        {
            if (_errors == 6)
            {
                await DisplayAlert("Perdeu!", "Você foi enforcado, quer começar um novo jogo?", "Novo Jogo");
                ResetScreen();
            }
        }
        #endregion

        #region Reset Screen - Back Screen to Initial State
        private void ResetScreen()
        {
            ResetErrors();
            GenerateNewWord();
            ResetVirtualKeyboard();
        }

        private void GenerateNewWord()
        {
            ImgMain.Source = ImageSource.FromFile("forca1.png");
            var repository = new WordRepository();
            _word = repository.GetRandomWord();
            LblTips.Text = _word.Tips;
            LblText.Text = new string('_', _word.Text.Length);
        }

        private void ResetErrors()
        {
            _errors = 0;
        }

        private void ResetVirtualKeyboard()
        {
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[0]);
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[1]);
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[2]);
        }

        private void ResetVirtualLines(HorizontalStackLayout horizontal)
        {
            foreach (Button button in horizontal.Children)
            {
                button.IsEnabled = true;
                button.Style = null;
            }
        }
        #endregion
        private void OnButtonClickedResetGame(System.Object sender, System.EventArgs e)
        {
            ResetScreen();
        }
    }
}
