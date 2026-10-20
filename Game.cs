using Godot;
using System;
using System.Threading.Tasks;

public partial class Game : Node2D
{
	Marker2D spawnPoint;
	private int coins = 0;
	private int Score = 0;
	Label scoreLabel;
	private Plasma Plasma;
	private Rocket Rocket;
	private Warning Warning;
	private static int BestScore = 0;
	private static int BestCoins = 0;
	private Label scoreLabel2;
	private Label BestCoins2;
	private Label BestScore2;


	[Export]  PackedScene PlasmaPackedScene {get; set;}
	[Export]  PackedScene CoinsPackedScene1 { get; set; }
	[Export]  PackedScene CoinsPackedScene2 { get; set; }
	[Export]  PackedScene CoinsPackedScene3 { get; set; }
	[Export]  PackedScene RocketPackedScene { get; set; }
	
	private PackedScene RocketWarningPackedScene =
    GD.Load<PackedScene>("res://Warning.tscn");
	


	public override void _Ready()
	{
 

   

    spawnPoint = GetNode<Marker2D>("PlasmaSpawnPoint");
    scoreLabel = GetNode<Label>("CanvasLayer/Coins");
    scoreLabel2 = GetNode<Label>("CanvasLayer2/Score");
	BestCoins2 = GetNode<Label>("CanvasLayer/BestCoins");
    BestScore2 = GetNode<Label>("CanvasLayer2/BestScore");
		
				
	}


	public override void _Process(double delta)
	{
		BestCoins2.Text = "Best Coins:" + BestCoins.ToString();
		BestScore2.Text = "Best Score:" + BestScore.ToString();
	}
	public void ScoreCount()
	{
		Score += 1;
		scoreLabel2.Text = "Score:" + Score.ToString();
	}

	public void CoinsCollect()
	{
		coins += 1;
		scoreLabel.Text = "Coins: " + coins.ToString();
		//GD.Print(coins);

	}

	public void OnBarryDeath()
	{
		
	BestScore = Math.Max(BestScore, Score);
    BestCoins = Math.Max(BestCoins, coins);

   //GD.Print("BestScore");

    GetTree().ReloadCurrentScene();

	//GD.Print(BestScore);

	}
	
	public void OnSpawnTimerTimeout2()
	{

	}



public void CoinSpawnTimer(){


//GD.Print("Spawn Pattern");

Random rand = new Random();

int offset = (rand.Next(25) - 15) * 20;
int choice = rand.Next(1, 4);

Vector2 mPosition = spawnPoint.Position;
mPosition.Y += offset;

Node2D pattern = null;

		
		switch (choice)
	{
		case 1:
			pattern = CoinsPackedScene1.Instantiate<Pattern1>();
			break;

		case 2:
			pattern = CoinsPackedScene2.Instantiate<Pattern2>();
			break;

		case 3:
			pattern = CoinsPackedScene3.Instantiate<Pattern3>();
			break;
	}

		pattern.Position = mPosition;
		AddChild(pattern);
}



public void OnSpawnTimerTimeout()
{
    //GD.Print("Spawn Plasma");
	Plasma = PlasmaPackedScene.Instantiate<Plasma>();
	Vector2 mPosition = spawnPoint.Position;
    Random rand = new Random();
    int offset = (rand.Next(25) - 15) * 20;
	int RotationOffset = rand.Next (0,360);

    mPosition.Y += offset;

  
    Plasma.Position = mPosition;
	Plasma.Rotation = Mathf.DegToRad(RotationOffset);

    AddChild(Plasma);
}

public async void  RocketSpawnTimer()
{
	//GD.Print("LAKJSLKAHSFjkhJ");
	Vector2 mPosition = spawnPoint.Position;
	Rocket = RocketPackedScene.Instantiate<Rocket>();	


	//GD.Print("test");
	
	Random rand = new Random();
	int offset = (rand.Next(25) - 15) * 20;
	mPosition.Y += offset;
	Rocket.Position = mPosition;
	mPosition.X -= 100;
	
	Warning = RocketWarningPackedScene.Instantiate<Warning>();
	Warning.Position =mPosition;
	
	AddChild(Warning);
	await Task.Delay(1000);
	Warning.QueueFree();
	AddChild(Rocket);

}


}
