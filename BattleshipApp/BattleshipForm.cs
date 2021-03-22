using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleshipLibrary;

namespace BattleshipApp
{
    [Serializable]
    public partial class BattleshipForm : Form
    {

        #region Constants

        const int HEIGTH = 10;
        const int WIDTH = 10;
        const int MAX_PIXEL_HEIGTH = 360;
        const int MAX_PIXEL_WIDTH_FIRST = 330;
        const int MAX_PIXEL_WIDTH_SECOND = 690;
        const int CELL_SIZE = 30;

        #endregion

        #region Private

        private ButtonMap[,] _buttonsPlayer;
        private ButtonMap[,] _buttonsEnemy;
        private RadioButton _buttonEasy;
        private RadioButton _buttonMidlle;
        private RadioButton _buttonHard;
        private Button _buttonGo;
        private Label _labelMessage;
        private Label _eventMessage;
        private RadioButton _buttonAuto = null;
        private RadioButton _buttonManual = null;
        private Button _buttonPlay;
        private Label _tableDirection;
        private DirectionButton[] _fourDirections;

        private readonly FormGame _currentGame;

        #endregion
        
        public BattleshipForm(FormGame currentGame)
        {
            InitializeComponent();
            _buttonsPlayer = new ButtonMap[WIDTH, HEIGTH];
            _buttonsEnemy = new ButtonMap[WIDTH, HEIGTH];
            _currentGame = currentGame;
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            NameOfGame.Visible = false;
            StartGame.Visible = false;
            ExitGame.Visible = false;
            BackgroundImage = null;

            AddDifficultyButton();
        }

        public void AddDifficultyButton()
        {
            _buttonEasy = new RadioButton();
            _buttonEasy.Location = new Point(300, 100);
            _buttonEasy.Size = new Size(200, 50);
            _buttonEasy.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonEasy.ForeColor = Color.Black;
            _buttonEasy.Text = "Easy";
            _buttonEasy.CheckedChanged += new EventHandler(CheckedEasyLevel);
            Controls.Add(_buttonEasy);

            _buttonMidlle = new RadioButton();
            _buttonMidlle.Location = new Point(300, 150);
            _buttonMidlle.Size = new Size(200, 50);
            _buttonMidlle.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonMidlle.ForeColor = Color.Black;
            _buttonMidlle.Text = "Medium";
            _buttonMidlle.CheckedChanged += new EventHandler(CheckedMediumLevel);
            Controls.Add(_buttonMidlle);

            _buttonHard = new RadioButton();
            _buttonHard.Location = new Point(300, 200);
            _buttonHard.Size = new Size(200, 50);
            _buttonHard.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonHard.ForeColor = Color.Black;
            _buttonHard.Text = "Hard";
            _buttonHard.CheckedChanged += new EventHandler(CheckedHardLevel);
            Controls.Add(_buttonHard);

            _buttonGo = new Button();
            _buttonGo.Location = new Point(250, 300);
            _buttonGo.Size = new Size(200, 50);
            _buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonGo.ForeColor = Color.Black;
            _buttonGo.Text = "GO";
            _buttonGo.Name = "Go";
            _buttonGo.Click += new EventHandler(AddShipsSettingButton);
            Controls.Add(_buttonGo);
        }

        public void AddShipsSettingButton(object sender, EventArgs e)
        {
            _buttonEasy.Visible = false;
            _buttonMidlle.Visible = false;
            _buttonHard.Visible = false;
            _buttonGo.Visible = false;

            _buttonAuto = new RadioButton();
            _buttonAuto.Location = new Point(300, 100);
            _buttonAuto.Size = new Size(200, 50);
            _buttonAuto.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonAuto.ForeColor = Color.Black;
            _buttonAuto.Text = "AutoSet";
            _buttonAuto.CheckedChanged += new EventHandler(GetAutoSetting);
            Controls.Add(_buttonAuto);

            _buttonManual = new RadioButton();
            _buttonManual.Location = new Point(300, 150);
            _buttonManual.Size = new Size(200, 50);
            _buttonManual.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonManual.ForeColor = Color.Black;
            _buttonManual.Text = "Manual";
            _buttonManual.CheckedChanged += new EventHandler(GetManualSetting);//
            Controls.Add(_buttonManual);

            _buttonPlay = new Button();
            _buttonPlay.Location = new Point(250, 300);
            _buttonPlay.Size = new Size(200, 50);
            _buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _buttonPlay.ForeColor = Color.Black;
            _buttonPlay.Text = "PLAY";
            _buttonPlay.Name = "Play";
            _buttonPlay.Click += new EventHandler(AddNameTextBox);
            Controls.Add(_buttonPlay);
        }

        private void ExitGame_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckedEasyLevel(object sender, EventArgs e)
        {
            _currentGame.IsEasyLevel = true;
        }

        private void CheckedMediumLevel(object sender, EventArgs e)
        {
            _currentGame.SetGameLevel(Level.Meduim);
        }

        private void CheckedHardLevel(object sender, EventArgs e)
        {
            _currentGame.SetGameLevel(Level.Hard);
        }

        private void GetAutoSetting(object sender, EventArgs e)
        {
            _currentGame.SetShipsAuto();
        }

        private void GetManualSetting(object sender, EventArgs e)
        {
            _currentGame.SetPlayerMapCondition();
        }

        private void AddNameTextBox(object sender, EventArgs e)
        {
            _buttonAuto.Visible = false;
            _buttonManual.Visible = false;
            _buttonPlay.Visible = false;

            _labelMessage = new Label();
            _labelMessage.Location = new Point(240, 150);
            _labelMessage.Size = new Size(300, 50);
            _labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _labelMessage.ForeColor = Color.Black;
            _labelMessage.Text = "Please, input your name:";
            Controls.Add(_labelMessage);

            TextBox name = new TextBox();
            name.Location = new Point(270, 200);
            name.Size = new Size(180, 70);
            name.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    11.75F, System.Drawing.FontStyle.Bold);
            name.ForeColor = Color.Black;
            name.BorderStyle = BorderStyle.Fixed3D;
            name.KeyPress += new KeyPressEventHandler(InputName);
            Controls.Add(name);
        }

        private void InputName(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                TextBox name = (TextBox)sender;
                _currentGame.Name = name.Text + "'s map";
                name.Visible = false;


                if (_currentGame.PlayerMap != null)
                {
                    SetPlayerGameMap();
                }
                else
                {
                    ShowPlayerMapForSettingShips();
                }
            }
        }

        private void ShowPlayerMapForSettingShips()
        {
            _labelMessage.Visible = false;
            BackColor = Color.Black;

            AddPlayersName();
            AddBorders(360, 50);
            AddButtonsCell(80, 390, MAX_PIXEL_WIDTH_SECOND, _buttonsPlayer, _currentGame.PlayerMapManualInput, false);
            PrintPlayerMap(_buttonsPlayer, string.Empty);
            SetDirectionButtons();
            AddTableDirection();
        }

        private void SetDirectionButtons()
        {
            _fourDirections = new DirectionButton[4];

            for (Direction i = Direction.Up; i <= Direction.Left; i++)
            {
                _fourDirections[(int)(i - 1)] = new DirectionButton(i);

                switch (i)
                {
                    case Direction.Up:
                        _fourDirections[(int)(i - 1)].Location = new Point(150, 100);
                        break;
                    case Direction.Right:
                        _fourDirections[(int)(i - 1)].Location = new Point(250, 200);
                        break;
                    case Direction.Down:
                        _fourDirections[(int)(i - 1)].Location = new Point(150, 300);
                        break;
                    case Direction.Left:
                        _fourDirections[(int)(i - 1)].Location = new Point(50, 200);
                        break;
                    default:
                        break;
                }

                _fourDirections[(int)(i - 1)].Size = new Size(100, 40);
                _fourDirections[(int)(i - 1)].Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
                _fourDirections[(int)(i - 1)].ForeColor = Color.Red;
                _fourDirections[(int)(i - 1)].Text = i.ToString();
                _fourDirections[(int)(i - 1)].Click += new EventHandler(ClickButtonDirection);
                _fourDirections[(int)(i - 1)].Visible = false;

                Controls.Add(_fourDirections[(int)(i - 1)]);

            }
        }

        private void ShowDirectionButtons()
        {
            for (int i = 0; i < 4; i++)
            {
                _fourDirections[i].Visible = true;
            }
        }

        private void HideDirectionButtons()
        {
            for (int i = 0; i < 4; i++)
            {
                _fourDirections[i].Visible = false;
            }
        }

        private void AddTableDirection()
        {
            _tableDirection = new Label();
            _tableDirection.Location = new Point(50, 100);
            _tableDirection.Size = new Size(300, 200);
            _tableDirection.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    13.75F, System.Drawing.FontStyle.Bold);
            _tableDirection.ForeColor = Color.Orange;
            _tableDirection.TextAlign = ContentAlignment.MiddleCenter;
            _tableDirection.BorderStyle = BorderStyle.FixedSingle;
            _tableDirection.Text = "PRESS ANY EMPTY CELL FOR SETTING SHIP ";
            Controls.Add(_tableDirection);
        }

        private void ClickSettingShipButton(object sender, EventArgs e)
        {
            ButtonMap currentButton = (ButtonMap)sender;
            _tableDirection.Text = (currentButton.Coord.OY + 1).ToString() + ((Letters)currentButton.Coord.OX).ToString();
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            _tableDirection.Text = "INPUT DIRECTION:";
            
            Application.DoEvents();


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _buttonsPlayer[i, j].Click -= ClickSettingShipButton;
                }
            }

            _currentGame.StartShipPosition = ((ButtonMap)sender).Coord;
            _tableDirection.Visible = false;

            ShowDirectionButtons();
        }

        public void ClickButtonDirection(object sender, EventArgs e)
        {
            _tableDirection.Visible = true;
            _tableDirection.Text = "PRESS ANY EMPTY CELL FOR SETTING SHIP ";

            Application.DoEvents();

            DirectionButton buttonDirection = (DirectionButton)sender;

            if (_currentGame.HasAllShipsPlayer())
            {
                return;
            }

            _currentGame.SetOneShip(buttonDirection.CurrentDirection);

            for (int OY = 0; OY < 10; OY++)
            {
                for (int OX = 0; OX < 10; OX++)
                {
                    _buttonsPlayer[OY, OX].CellCondition = _currentGame.GetMapManualCondition(OY, OX);
                }
            }

            for (int OY = 0; OY < 10; OY++)
            {
                for (int OX = 0; OX < 10; OX++)
                {
                    _buttonsPlayer[OY, OX].Click += ClickSettingShipButton;
                }
            }

            PrintPlayerMap(_buttonsPlayer, string.Empty);
            HideDirectionButtons();

            if (_currentGame.HasAllShipsPlayer())
            {
                _tableDirection.Visible = false;

                for (int OY = 0; OY < 10; OY++)
                {
                    for (int OX = 0; OX < 10; OX++)
                    {
                        _buttonsPlayer[OY, OX].Click -= ClickSettingShipButton;
                    }
                }

                _currentGame.PlayerMap = _currentGame.PlayerMapManualInput;
                _currentGame.PlayerMap.InjuredShip += ChangeMapCellInjuredPlayer;
                _currentGame.PlayerMap.DestroyedShip += ChangeMapCellDestroyedPlayer;
                _currentGame.PlayerMap.MissedCell += ChangeMapCellPastPlayer;

                SetEnemyGameMap();
            }
        }

        private void SetPlayerGameMap() 
        {
            _currentGame.PlayerMap.InjuredShip += ChangeMapCellInjuredPlayer;
            _currentGame.PlayerMap.DestroyedShip += ChangeMapCellDestroyedPlayer;
            _currentGame.PlayerMap.MissedCell += ChangeMapCellPastPlayer;

            AddBorders(360, 50);
            AddButtonsCell(80, 390, MAX_PIXEL_WIDTH_SECOND, _buttonsPlayer, _currentGame.PlayerMap, false);
            PrintPlayerMap(_buttonsPlayer, string.Empty);
            SetEnemyGameMap();
        }

        private void SetEnemyGameMap()
        {
            _labelMessage.Visible = false;
            BackColor = Color.Black;

            _currentGame.EnemyMap = new Sea(RandomCoords.MAP_SIZE);
            _currentGame.SetEnemyMap();

            _currentGame.EnemyMap.InjuredShip += ChangeMapCellInjuredEnemy;
            _currentGame.EnemyMap.DestroyedShip += ChangeMapCellDestroyedEnemy;
            _currentGame.EnemyMap.MissedCell += ChangeMapCellPastEnemy;

            AddEnemysName();
            AddShowMessage();
            AddPlayersName();
            AddBorders(10, 50);
            AddButtonsCell(80, 40, MAX_PIXEL_WIDTH_FIRST, _buttonsEnemy, _currentGame.EnemyMap, true);
            PrintEnemyMap(_buttonsEnemy, string.Empty);
        }
      
        private void AddEnemysName()
        {
            Label enemysName = new Label();
            enemysName.Location = new Point(100, 15);
            enemysName.Size = new Size(180, CELL_SIZE);
            enemysName.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    11.75F, System.Drawing.FontStyle.Bold);
            enemysName.Text = "Enemy's map";
            enemysName.TextAlign = ContentAlignment.MiddleCenter;
            enemysName.ForeColor = Color.Orange;
            enemysName.BorderStyle = BorderStyle.None;
            Controls.Add(enemysName);
        }

        private void AddPlayersName()
        {
            Label playerName = new Label();
            playerName.Location = new Point(460, 15);
            playerName.Size = new Size(180, CELL_SIZE);
            playerName.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    11.75F, System.Drawing.FontStyle.Bold);
            playerName.Text = _currentGame.Name;
            playerName.TextAlign = ContentAlignment.MiddleCenter;
            playerName.ForeColor = Color.Orange;
            playerName.BorderStyle = BorderStyle.None;
            Controls.Add(playerName);
        }

        private void AddBorders(int width, int heigth)
        {
            int counter = 0;

            for (int i = (width + CELL_SIZE); i <= width + (WIDTH * CELL_SIZE); i += CELL_SIZE)
            {
                Label letter = new Label();
                letter.Location = new Point(i, heigth);
                letter.Size = new Size(CELL_SIZE, CELL_SIZE);
                letter.Font = new System.Drawing.Font("Microsoft Sans Serif",
                        11.75F, System.Drawing.FontStyle.Bold);
                letter.ForeColor = Color.Orange;
                letter.Text = ((Letters)counter).ToString();
                letter.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(letter);
                counter++;
            }

            counter = 1;

            for (int i = (heigth + CELL_SIZE); i <= (heigth + HEIGTH * CELL_SIZE); i += CELL_SIZE)
            {
                Label number = new Label();
                number.Location = new Point(width, i);
                number.Size = new Size(CELL_SIZE, CELL_SIZE);
                number.Font = new System.Drawing.Font("Microsoft Sans Serif",
                        11.75F, System.Drawing.FontStyle.Bold);
                number.ForeColor = Color.Orange;
                number.Text = counter.ToString();
                number.TextAlign = ContentAlignment.MiddleCenter;
                Controls.Add(number);
                counter++;
            }
        }

        private void AddButtonsCell(int startCoordY, int startCoordX, int maxWidth, 
                ButtonMap[,] buttonsCell, Sea map, bool isEnemyClickButton)
        {
            int oY = 0;
            int oX = 0;

            for (int Y = startCoordY; Y < MAX_PIXEL_HEIGTH; Y += CELL_SIZE)
            {
                for (int X = startCoordX; X < maxWidth; X += CELL_SIZE)
                {
                    ButtonMap buttonCell = new ButtonMap(oX, oY);
                    buttonCell.Location = new Point(X, Y);
                    buttonCell.Size = new Size(CELL_SIZE, CELL_SIZE);
                    buttonCell.Font = new System.Drawing.Font("Microsoft Sans Serif",
                            11.75F, System.Drawing.FontStyle.Bold);
                    buttonCell.FlatStyle = FlatStyle.Flat;
                    buttonCell.FlatAppearance.BorderSize = 2;
                    buttonCell.FlatAppearance.BorderColor = Color.Orange;
                    buttonCell.CellCondition = map[oY, oX];

                    buttonsCell[oY, oX] = buttonCell;

                    Controls.Add(buttonCell);

                    if (isEnemyClickButton)
                    {
                        buttonCell.Click += new EventHandler(ClickButtonCellEnemy);
                    }
                    else
                    {
                        buttonCell.Click += new EventHandler(ClickSettingShipButton);
                    }
                    
                    oX++;
                }

                oX = 0;
                oY++;
            }

        }

        private void PrintPlayerMap(ButtonMap[,] buttons, string message)
        {
            for (int i = 0; i < HEIGTH; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    switch (buttons[i, j].CellCondition)
                    {
                        case MapCondition.MissedShot:
                            buttons[i, j].Image = Properties.Resources.PastCellNew;
                            break;
                        case MapCondition.NoneShot:
                            buttons[i, j].Image = Properties.Resources.WaveCell;
                            break;
                        case MapCondition.ShipSafe:
                            buttons[i, j].Image = Properties.Resources.ShipCell;
                            break;
                        case MapCondition.ShipInjured:
                            buttons[i, j].Image = Properties.Resources.BombCellNew;
                            break;
                        case MapCondition.ShipDestroyed:
                            buttons[i, j].Image = Properties.Resources.DestructionCell;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void PrintEnemyMap(ButtonMap[,] buttons, string message)
        {
            for (int i = 0; i < HEIGTH; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    switch (buttons[i, j].CellCondition)
                    {
                        case MapCondition.MissedShot:
                            buttons[i, j].Image = Properties.Resources.PastCellNew;
                            break;
                        case MapCondition.NoneShot:
                            buttons[i, j].Image = Properties.Resources.WaveCell;
                            break;
                        case MapCondition.ShipSafe:
                            buttons[i, j].Image = Properties.Resources.WaveCell;
                            break;
                        case MapCondition.ShipInjured:
                            buttons[i, j].Image = Properties.Resources.BombCellNew;
                            break;
                        case MapCondition.ShipDestroyed:
                            buttons[i, j].Image = Properties.Resources.DestructionCell;
                            break;
                        default:
                            break;
                    }
                }
            }

            _eventMessage.Text = message;
        }

        private void ClickButtonCellEnemy(object sender, EventArgs e)
        {
            ButtonMap cellButton = (ButtonMap)sender;
            _currentGame.EnemyMap.TargetCoordX = cellButton.Coord.OX;
            _currentGame.EnemyMap.TargetCoordY = cellButton.Coord.OY;

            bool wasShot = _currentGame.EnemyMap.WasShot();

            if (wasShot)
            {
                MessageBox.Show("You alredy have shoot this position!");
            }
            else
            {
                IsPlayerTurn();
            }

        }

        private void IsPlayerTurn()
        {
            string message;
            bool isEnemyTurn;
            bool isPlayerWin;

            Application.DoEvents();

            _currentGame.IsPlayerTurn(out message, out isEnemyTurn, out isPlayerWin);

            if (_currentGame.IsTargetEnemy)
            {
                _eventMessage.Text = message;
            }

            if (isPlayerWin)
            {
                _eventMessage.Text = message;
                MessageBox.Show(message);
                Application.Restart();
            }

            if (isEnemyTurn)
            {
                Application.DoEvents();
                _eventMessage.Text = message;
                EnemysTurn();
            }
        }

        private void EnemysTurn()
        {
            string message;
            bool isEnemyWin;

            if (_currentGame.PlayerMap.SearchShips())
            {
                do
                {
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(2000);

                    _currentGame.IsEnemyTurn(out message, out isEnemyWin);

                    Application.DoEvents();
                    _eventMessage.Text = message;
                    
                    

                    if (isEnemyWin)
                    {
                        MessageBox.Show(message);
                        Application.Restart();
                    }

                } while (_currentGame.IsTargetPlayer);

            }
        }

        private void AddShowMessage()
        {
            _eventMessage = new Label();
            _eventMessage.Location = new Point(190, 390);
            _eventMessage.Size = new Size(350, 80);
            _eventMessage.Font = new System.Drawing.Font("Microsoft Sans Serif",
                    11.75F, System.Drawing.FontStyle.Bold);
            _eventMessage.ForeColor = Color.Orange;
            _eventMessage.TextAlign = ContentAlignment.MiddleCenter;
            _eventMessage.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(_eventMessage);
        }

        public void ChangeMapCellInjuredPlayer(object sender, InjuredShipEventArgs e)
        {
            _buttonsPlayer[e.InjuredCell.OY, e.InjuredCell.OX].Image = Properties.Resources.BombCellNew;
        }

        public void ChangeMapCellDestroyedPlayer(object sender, DestroyedShipEventArgs e)
        {
            for (int i = 0; i < e.DestroyedShip.CountOfDeck; i++)
            {
                _buttonsPlayer[e.DestroyedShip[i].OY, e.DestroyedShip[i].OX].Image = Properties.Resources.DestructionCell;
            }
        }

        public void ChangeMapCellPastPlayer(object sender, MissedShotEventArgs e)
        {
            _buttonsPlayer[e.MissedPosition.OY, e.MissedPosition.OX].Image = Properties.Resources.PastCellNew;
        }

        public void ChangeMapCellInjuredEnemy(object sender, InjuredShipEventArgs e)
        {
            _buttonsEnemy[e.InjuredCell.OY, e.InjuredCell.OX].Image = Properties.Resources.BombCellNew;
        }

        public void ChangeMapCellDestroyedEnemy(object sender, DestroyedShipEventArgs e)
        {
            for (int i = 0; i < e.DestroyedShip.CountOfDeck; i++)
            {
                _buttonsEnemy[e.DestroyedShip[i].OY, e.DestroyedShip[i].OX].Image = Properties.Resources.DestructionCell;
            }
        }

        public void ChangeMapCellPastEnemy(object sender, MissedShotEventArgs e)
        {
            _buttonsEnemy[e.MissedPosition.OY, e.MissedPosition.OX].Image = Properties.Resources.PastCellNew;
        }
    }
}
