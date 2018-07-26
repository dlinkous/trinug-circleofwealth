﻿using System;

namespace CircleOfWealth.ConsoleApplication
{
	internal static class HiddenData
	{
		private const string phrases = @"HUNT AND PECK
HIT THE GROUND RUNNING
NOTHING VENTURED, NOTHING GAINED
ONE SMALL STEP FOR MAN
BREAK THE ICE
FAST ASLEEP
ELEPHANT IN THE ROOM
THROW IN THE TOWEL
KEEP THE BALL ROLLING
TURN THE TABLES
COUNTING SHEEP
ONCE IN A BLUE MOON
ALL YOU CAN EAT
TOOTH AND NAIL
FOREGONE CONCLUSION
TOMORROW IS ANOTHER DAY
PUSH THE ENVELOPE
NIGHT OWL
WHEN PIGS FLY
WALK THE PLANK
JACK OF ALL TRADES, MASTER OF NONE
GOOD AS GOLD
WILD GOOSE CHASE
BARKING UP THE WRONG TREE
FAIR AND SQUARE
AS THE CROW FLIES
COOKIE CUTTER
BITE THE DUST
BUSY AS A BEE
TWO CENTS WORTH
TAKE A BACK SEAT
PUT YOUR BEST FOOT FORWARD
ACROSS THE BOARD
RISE AND SHINE
START FROM SCRATCH
MAKE MY DAY
PIE IN THE SKY
RUN THE GAUNTLET
LET THE CAT OUT OF THE BAG
GREASED LIGHTNING
BATTEN DOWN THE HATCHES";
		private static Random random = new Random();

		internal static string GetPhrase()
		{
			var lines = phrases.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			var line = lines[random.Next(0, lines.Length)];
			return line;
		}
	}
}