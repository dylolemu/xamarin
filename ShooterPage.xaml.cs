using Xamarin.Forms;
using System;

namespace Shooter
{
	public partial class ShooterPage : ContentPage
	{
		public ShooterPage()
		{
			InitializeComponent();

			//sets the color of the buttons
			leftButton.BackgroundColor = Color.Transparent;
			rightButton.BackgroundColor = Color.Transparent;
			fireButton.BackgroundColor = Color.Transparent;

			enemyBullet.TranslationX = 176;
			scoreLabel.TranslationX = 150;

			//starts and creates timer
			var milli = TimeSpan.FromMilliseconds(40);

			Device.StartTimer(milli, () =>
			{
				testMethod();
				return true;
			});

			shoot = false;
			start = false;
		}

		Random rand = new Random();

		//declaring all values for locations of jet, enemies, bullets and number of lives
		int v = 165;
		int vy = 500;
		int enemyv = 165;
		int enemyvy = 80;
		int enemyv2 = 165;
		int enemyvy2 = 80;
		int bulletx, bulletx2;
		int random;
		int random2;
		int highScore;
		bool shoot;
		bool start;
		bool hit, hit2;
		int score;
		int lives = 3;
		int speed = 11;
		int bulletLocation;

		public void leftButt_Clicked(object sender, EventArgs args)
		{
			//move ship left
			if (v > 0 && lives > 0)
			{
				v = v - 30;
			}
			//picks random location for enemy to move
			random = rand.Next(1, 400);
			random2 = rand.Next(1, 400);
			hit = false;
			hit2 = false;
		}


		public void rightButt(object sender, EventArgs args)
		{
			//move ship right
			if (v < this.Width - Ship.Width && lives > 0)
			{
				v = v + 30;
			}
			//picks random location for enemy to move
			random = rand.Next(1, 400);
			random2 = rand.Next(1, 400);
			hit = false;
			hit2 = false;
		}

		public void fireButt(object sender, EventArgs args)
		{
			//if game has not started
			if (start == false)
			{
				//sets location for all buttons
				fireButton.TranslationX = (this.Width / 2) - (fireButton.Width / 2);
				fireButton.TranslationY = this.Height - 100;
				rightButton.TranslationX = this.Width - leftButton.Width;
				rightButton.TranslationY = this.Height - rightButton.Height;
				leftButton.TranslationX = 0;
				leftButton.TranslationY = this.Height - leftButton.Height;

				title.TranslationY = 800;
				start = true;
				lives = 3;
			}
			if (shoot == false)
			{
				//bullet movement
				bulletLocation = v + speed;
				bullet.TranslationX = bulletLocation;
			}
			shoot = true;
			hit = false;
			hit2 = false;
		}

		public void enemies()
		{
			//if enemy one is hit
			if (hit == true)
			{
				enemy.TranslationY = -800;
				enemyBullet.TranslationY = -800;
				enemyDeath.TranslationY = 80;
			}
			else
			{
				enemy.TranslationY = 80;
				enemyDeath.TranslationY = -80;

				//enemy ship movement
				if (enemyv > 0)
				{
					if (enemyv > random)
					{
						enemyv = enemyv - speed;
					}
				}

				if (enemyv < this.Width - enemy.Width)
				{
					if (enemyv < random)
					{
						enemyv = enemyv + speed;
					}
				}

				//enemy bullet movement
				if (enemyvy > this.Height)
				{
					enemyvy = 80;
					enemyBullet.TranslationX = enemyv;
					bulletx = enemyv;
					if (hit == false)
					{
						random = rand.Next(1, 400);
					}
				}
				else
				{
					enemyvy = enemyvy + speed;
				}
			}
			//if enemy 2 is hit
			if (hit2 == true)
			{
				enemy2.TranslationY = -800;
				enemyBullet2.TranslationY = -800;
				enemyDeath2.TranslationY = 80;
			}
			else
			{
				//if you have killed x number of enemies then new enemy appears
				if (score > 1)
				{
					enemy2.TranslationY = 80;

					if (enemyvy2 > this.Height)
					{
						enemyBullet2.TranslationX = enemyv2;
						bulletx2 = enemyv2;
						enemyvy2 = 80;

						if (hit2 == false)
						{
							//picks random location for enemy to move
							random2 = rand.Next(1, 400);
						}
					}
					else
					{
						enemyvy2 = enemyvy2 + speed;
					}
					//enemy movement
					if (enemyv2 > 0)
					{
						if (enemyv2 > random2)
						{
							enemyv2 = enemyv2 - speed;
						}
					}

					if (enemyv2 < this.Width - enemy2.Width)
					{
						if (enemyv2 < random2)
						{
							enemyv2 = enemyv2 + speed;
						}
					}
					enemyBullet2.TranslationY = enemyvy2;

					if (bulletLocation + bullet.Width > enemyv2 && bulletLocation < enemyv2 + enemy2.Width && vy + bullet.Height > 80 && vy < 80 + enemy2.Height)
					{
						score++;
						vy = -500;
						hit2 = true;
					}
					enemy2.TranslationY = 80;
					enemyDeath2.TranslationY = -80;
				}
				if (score < 2)
				{ enemy2.TranslationY = -80; }
			}
		}

		public void gameOver()
		{
			if (lives == 2)
			{
				live3.TranslationY = -100;
			}
			if (lives == 1)
			{
				live2.TranslationY = -100;
				live3.TranslationY = -100;
			}

			//if you lose all your lives
			if (lives <= 0)
			{
				//resets all variables back to original
				start = false;
				score = 0;
				fireButton.TranslationX = 57;
				fireButton.TranslationY = 230;
				enemy2.TranslationY = -800;
				enemyBullet2.TranslationY = -800;
				enemyBullet.TranslationY = -800;
				enemyBullet.TranslationX = 176;
				v = 165;
				vy = 500;
				enemyv = 165;
				enemyvy = 80;
				live1.TranslationY = 20;
				live2.TranslationY = 20;
				live3.TranslationY = 20;
				enemyDeath.TranslationY = -80;
				enemyDeath2.TranslationY = -80;
				title.TranslationY = 0;
			}
		}
		public void testMethod()
		{
			//ships movements
			Ship.TranslationX = v;
			enemy.TranslationX = enemyv;
			enemy2.TranslationX = enemyv2;
			enemyBullet.TranslationY = enemyvy;
			enemyDeath.TranslationX = enemyv;
			enemyDeath2.TranslationX = enemyv2;

			gameOver();

			if (score > highScore) { highScore = score; }

			scoreLabel.Text = "KILLS:  " + Convert.ToString(score) + "    HIGH SCORE:  " + Convert.ToString(highScore) + "   ";

			if (start == true)
			{
				enemies();

				if (shoot == true)
				{
					vy = vy - speed;
					bullet.TranslationY = vy;
					if (vy < 0 - bullet.Height)
					{
						vy = 500;
						shoot = false;
					}
				}

				//if you hit enemy1 with your bullet
				if (bulletLocation + bullet.Width > enemyv && bulletLocation < enemyv + enemy.Width && vy + bullet.Height > 80 && vy < 80 + enemy.Height)
				{
					score++;
					vy = -500;
					hit = true;
				}

				//if you get hit by enemy bullets
				if (bulletx + enemyBullet.Width > v && bulletx < v + Ship.Width && enemyvy + enemyBullet.Height > 500 && enemyvy < 500 + Ship.Height)
				{
					enemyvy = 800;
					lives--;
				}

				if (bulletx2 + enemyBullet2.Width > v && bulletx2 < v + Ship.Width && enemyvy2 + enemyBullet2.Height > 500 && enemyvy2 < 500 + Ship.Height)
				{
					enemyvy2 = 800;
					lives--;
				}
			}
		}
	}
}
