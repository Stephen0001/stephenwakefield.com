<?php
class Deck {

	protected $gameDeck;

	public $type = array(
		"spades",
		"hearts",
		"clubs",
		"diamonds"
	); 

	public $value = array();
	
	public $cardArray = array();

	final public function createDeck() {
		$suitesLowestValue = 2;
		$suitesHighestValue = 14;
		$singleDigitNumbers = 10;
		$numberOfPlayers = 2;

		for ($i=$suitesLowestValue; $i <= $suitesHighestValue; $i++) {
			if ($i < $singleDigitNumbers) {
				$this->value[] = 0 . $i;
			} else {
				$this->value[] = $i;
			}
		}

		$output = "";

		for ($j=0; $j < count($this->value); $j++) { 
			for ($k=0; $k < count($this->type); $k++) { 
				$output .= $this->value[$j];
				$output .= "_of_";
				$output .= $this->type[$k];
				$output .= "  ";
			}	//end of second for loop	

		}	//end of first for loop

		$this->cardArray = $output;

		$this->cardArray = explode("  ",$this->cardArray);

		array_pop($this->cardArray);

		//randomize the deck
		shuffle($this->cardArray);

		//split the deck into two piles
		$this->gameDeck = array_chunk($this->cardArray,count($this->cardArray)/$numberOfPlayers);

		return $this->gameDeck;

	}	//end of method

}   //end of class
?>

<?php
class Player {
	public $player;

	public function set_name($new_name) {
		$this->name = $new_name;
	}

	public function get_name() {
		return $this->name;
	}
} //end of class
?>

<?php 
class Game {
	public $substringLength = 2;

	public function getCardVal($playersDeck) {
		return substr($playersDeck[0],0,$this->substringLength);
	}

	public function displayTieImgs($playersDeck,$tie) {
		$output = "<td>";
		for ($i=1; $i < $tie; $i++) {
			$output .= "<img src=\"img/";
			$output .= $playersDeck[$i];
			$output .= ".jpg\">";
		}
		$output .= "</td>";
		echo $output;
	}

	public function winnerTakesSpoils(&$winner,&$loser,&$player1Deck,&$player2Deck) {
		array_push($winner, $loser[0]);
		array_push($winner, $winner[0]);
		array_splice($winner,0,1);
		array_splice($loser,0,1);
		echo "<td>" . count($player1Deck) . "</td>";
		echo "<td>" . count($player2Deck) . "</td>";
	}

	public function tieBreaker($message,&$winner,&$loser,&$player1Deck,&$player2Deck,$tie) {
		$this->displayTieImgs($player1Deck,$tie);
		$this->displayTieImgs($player2Deck,$tie);
		echo "<td>$message</td>";

		for ($i=0; $i < $tie; $i++) { 
			array_push($winner,$winner[$i]);
			array_push($winner,$loser[$i]);
		}
		array_splice($winner,0,$tie);
		array_splice($loser,0,$tie);
	}

	public function stalemate(&$player1Deck,&$player2Deck,$tie) {
		$this->displayTieImgs($player1Deck,$tie);
		$this->displayTieImgs($player2Deck,$tie);
		echo "<td>Stalemate</td>";
		for ($i=0; $i < $tie; $i++) { 
			array_push($player2Deck,$player2Deck[$i]);
			array_push($player1Deck,$player1Deck[$i]);
		}
		array_splice($player2Deck,0,$tie);
		array_splice($player1Deck,0,$tie);
	}
} //end of class
?>

<?php //include "includes/class_lib.php"; ?>
<!doctype HTML>
<html lang="en">

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>OOP WAR</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <div>
		<?php
		$player = new Player();
		$deck = new Deck();
		$game = new Game();

		$fullDeck = $deck->createDeck();

		$player1Deck = $fullDeck[0];
		$player2Deck = $fullDeck[1];

		if (isset($_GET["submit"])) {
			$player->set_name(htmlentities(($_GET['player1'])));
			$player1 = $player->get_name();
			$player->set_name(htmlentities(($_GET['player2'])));
			$player2 = $player->get_name();

			echo 
			"
			<table>
				<tr>
					<th>$player1's Card</th>
					<th>$player2's Card</th>
					<th>$player1's Pile</th>
					<th>$player2's Pile</th>
					<th colspan='3'>Tie Breaker</th>
				</tr>
				<tr>
					<td colspan='2'>START:</td>
					<td>" . count($player1Deck) . "</td>
					<td>" . count($player2Deck) . "</td>
				</tr>";

				$battleCount = 1;
				$substringLength = 2;
				$limit = 1000;
				$lastCard = 1;
				$limitReached = false;
				
				while(count($player1Deck) > 0 && count($player2Deck) > 0) {
					$tieBreaker = 4;
					$tie = 5;
					echo "<tr>
						<td><img src=\"img/$player1Deck[0].jpg\"></td>
						<td><img src=\"img/$player2Deck[0].jpg\"></td>";
						//if player1 wins
						if ($game->getCardVal($player1Deck) > $game->getCardVal($player2Deck)) {
							$game->winnerTakesSpoils($player1Deck,$player2Deck,$player1Deck,$player2Deck);
						//if player2 wins			
						} elseif ($game->getCardVal($player2Deck) > $game->getCardVal($player1Deck)) {
							$game->winnerTakesSpoils($player2Deck,$player1Deck,$player1Deck,$player2Deck);
						//if tie
						} else {
							//put this code into the Game class???
							echo "<td>" . count($player1Deck) . "</td>";
							echo "<td>" . count($player2Deck) . "</td>";
							if (count($player1Deck) <= $tieBreaker) {
								$tieBreaker = count($player1Deck) - 1;
								$tie = count($player1Deck);
							} elseif  (count($player2Deck) <= $tieBreaker) {
								$tieBreaker = count($player2Deck) - 1;
								$tie = count($player2Deck);
							}
							$player1TieBreaker = $player1Deck[$tieBreaker];
							$player2TieBreaker = $player2Deck[$tieBreaker];
								//if player1 wins the tie
								if (substr($player1TieBreaker,0,$substringLength) > substr($player2TieBreaker,0,$substringLength)) {
									$game->tieBreaker("Advantage $player1",$player1Deck,$player2Deck,$player1Deck,$player2Deck,$tie);
								//if player2 wins the tie
								} elseif (substr($player1TieBreaker,0,$substringLength) < substr($player2TieBreaker,0,$substringLength)) {
									$game->tieBreaker("Advantage $player2",$player2Deck,$player1Deck,$player1Deck,$player2Deck,$tie);
								//if there are two ties in a row (i.e. stalemate)
								} else {
									$game->stalemate($player1Deck,$player2Deck,$tie);
								}
						}	
				echo "</tr>";
				$battleCount++;
				if ($battleCount >= $limit && (count($player1Deck) == $lastCard || count($player2Deck) == $lastCard)) {
					$limitReached = true;
					break;
				}
				}  //end of while loop
			echo "</table>";
			echo "<h2>Battle Count is {$battleCount}</h2>";
			if ($limitReached) {
				echo "<h2>Battle limit has been reached!</h2>";
			}
			//Congratulation the winner!
			$winner = (count($player1Deck) > count($player2Deck)) ? "<h2>Congratulations {$player1}! ($player2 ran out of cards)</h2>" : "<h2>Congratulations {$player2}! ($player1 ran out of cards)</h2>";	
			echo $winner;
		}

		if(isset($_GET["submit"])) {
			echo "<h2>How About A Rematch?</h2>";
		}
		?>
        <form action="<?php echo htmlspecialchars($_SERVER["PHP_SELF"]);?>">
        <h3>Let's Play War!</h3>
            Player1:
            <input type="text" name="player1" value="Stephen" maxlength="10">
            <br> Player2:
            <input type="text" name="player2" value="Player2" maxlength="10" placeholder="Please enter your name">
            <br>
            <input type="submit" name="submit" value="submit">
        </form>
    </div>
</body>

</html>
