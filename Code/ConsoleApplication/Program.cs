using System;
using SimpleInjector;
using CircleOfWealth.Entities;
using CircleOfWealth.Interactors;
using CircleOfWealth.Interactors.Common.Boundaries;
using CircleOfWealth.Interactors.Common.Providers;
using CircleOfWealth.Interactors.Common.Repositories;
using CircleOfWealth.Interactors.UnitTests.Mocks;

namespace CircleOfWealth.ConsoleApplication
{
	public class Program
	{
		private static readonly Container container = new Container();

		public static void Main(string[] args)
		{
			FillContainer();
			Console.WriteLine("Welcome to Circle of Wealth!");
			Console.WriteLine("Please enter each player name, separating each name with a pipe.");
			var playerNamesLine = Console.ReadLine();
			var playerNames = playerNamesLine.Split("|");
			var startNewGameResponse = GetResponse<StartNewGame.RequestModel, StartNewGame.ResponseModel>(new StartNewGame.RequestModel()
			{
				PlayerNames = playerNames
			});
			var gameIdentifier = startNewGameResponse.GameIdentifier;
			var currentPhrase = startNewGameResponse.Phrase;
			var currentPlayerName = startNewGameResponse.CurrentPlayerName;
			while (true)
			{
				ConsoleKey key;
				do
				{
					Console.Clear();
					Console.WriteLine("Phrase:");
					Console.WriteLine(currentPhrase);
					Console.WriteLine("Player:");
					Console.WriteLine(currentPlayerName);
					Console.WriteLine("\t S = Spin \t\t G = Guess");
					key = Console.ReadKey(true).Key;
				} while (key != ConsoleKey.S && key != ConsoleKey.G);
				switch (key)
				{
					case ConsoleKey.S:
						var spinCircleResponse = GetResponse<SpinCircle.RequestModel, SpinCircle.ResponseModel>(new SpinCircle.RequestModel()
						{
							GameIdentifier = gameIdentifier
						});
						Console.WriteLine(spinCircleResponse.ResultDescription);
						if (spinCircleResponse.RequestLetter)
						{
							char keyChar;
							do
							{
								Console.WriteLine("Pick a letter");
								keyChar = Console.ReadKey(true).KeyChar;
							} while (!char.IsLetter(keyChar));
							keyChar = char.ToUpper(keyChar);
							var pickLetterResponse = GetResponse<PickLetter.RequestModel, PickLetter.ResponseModel>(new PickLetter.RequestModel()
							{
								GameIdentifier = gameIdentifier,
								Letter = keyChar
							});
							currentPhrase = pickLetterResponse.Phrase;
							currentPlayerName = pickLetterResponse.CurrentPlayerName;
							continue;
						}
						else
						{
							Console.WriteLine("Press a key to continue...");
							Console.ReadKey();
							currentPlayerName = spinCircleResponse.CurrentPlayerName;
							continue;
						}
					case ConsoleKey.G:
						Console.WriteLine("Enter the guess:");
						var guess = Console.ReadLine().ToUpper();
						var guessPhraseResponse = GetResponse<GuessPhrase.RequestModel, GuessPhrase.ResponseModel>(new GuessPhrase.RequestModel()
						{
							GameIdentifier = gameIdentifier,
							Guess = guess
						});
						if (guessPhraseResponse.Reward > 0)
						{
							Console.WriteLine($"Correct! {guessPhraseResponse.CurrentPlayerName} wins {guessPhraseResponse.Reward}!");
							Console.ReadKey();
							Environment.Exit(0);
							break;
						}
						else
						{
							Console.WriteLine("Incorrect!  Press a key to continue...");
							Console.ReadKey();
							currentPlayerName = guessPhraseResponse.CurrentPlayerName;
							continue;
						}
					default:
						throw new NotSupportedException();
				}
			}
		}

		private static void FillContainer()
		{
			container.RegisterSingleton<IRandomProvider, DotNetRandomProvider>();
			container.RegisterSingleton<ICircleRepository, MockCircleRepository>();
			container.RegisterSingleton<IGameRepository, MockGameRepository>();
			container.Register<IPhraseRepository, MockPhraseRepository>();
			container.Register(typeof(IOutputBoundary<>), typeof(IOutputBoundary<>).Assembly);
			container.Register(typeof(IInputBoundary<,>), typeof(IInputBoundary<,>).Assembly);
			container.RegisterInitializer<MockCircleRepository>(r =>
			{
				r.Circle = new Circle();
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Bankrupt, 0));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.LoseTurn, 0));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 500));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 600));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 700));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 800));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 900));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1000));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1100));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1200));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1300));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1400));
				r.Circle.AddSpace(new CircleSpace(CircleSpace.SpaceType.Dollar, 1500));
			});
			container.RegisterInitializer<MockPhraseRepository>(r =>
			{
				r.Phrase = HiddenData.GetPhrase();
			});
			container.Verify();
		}

		private static TResponseModel GetResponse<TRequestModel, TResponseModel>(TRequestModel requestModel)
		{
			var interactor = container.GetInstance<IInputBoundary<TRequestModel, TResponseModel>>();
			var presenter = new Presenter<TResponseModel>();
			interactor.HandleRequest(requestModel, presenter);
			return presenter.ResponseModel;
		}
	}
}
