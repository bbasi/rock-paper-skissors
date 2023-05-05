<?php
header('Content-Type: application/json');
// header('Access-Control-Allow-Origin: *'); // local testing

// set by bbnetconfig.php
$servername = "";
$username = "";
$password = "";
$dbname = "";

include '../cfg/bbnetconfig.php';

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
  die("connection failed: " . $conn->connect_error);
}

if ($_SERVER['REQUEST_METHOD'] === 'POST' && isset($_POST['setCoins'])) {
  $response = addScore($conn);
} else if (isset($_GET['method'])) {
  $method = $_GET['method'];
  switch ($method) {
      case 'getScores':
          $response = getTop10Scores($conn);
          break;
      default:
          $response = array('error' => 'err1');
          break;
  }
} else {
  $response = array('error' => 'err2');
}


echo json_encode($response);
exit();


function addScore($conn) {
  if (isset($_REQUEST["username"]) && isset($_REQUEST["coins"])) {
    $username = $_REQUEST['username'];
    $coins = $_REQUEST['coins'];

    $stmt = $conn->prepare("SELECT coinsHigh FROM rps_scores WHERE username = ?");
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $result = $stmt->get_result();
    if ($result->num_rows > 0) {  // user - existing
      $row = $result->fetch_assoc();
      if ($coins > $row['coinsHigh']) { // user - new high score
        $stmt = $conn->prepare("UPDATE rps_scores SET coinsHigh = ? WHERE username = ?");
        $stmt->bind_param("is", $coins, $username);
        $stmt->execute();
        $response = array('success' => true);
      } else {
        $response = array('success' => false, 'message' => 'err3');
      }
    } else {  // user - new
      $stmt = $conn->prepare("INSERT INTO rps_scores (username, coinsHigh) VALUES (?, ?)");
      $stmt->bind_param("si", $username, $coins);
      if ($stmt->execute()) {
        $response = array('success' => true);
      } else {
        $response = array('error' => 'err4');
      }
    }
    $stmt->close();
  } else {
    $response = array('error' => 'err5');
  }
  return $response;
}

function getTop10Scores($conn) {
  $sql = "SELECT username, coinsHigh FROM rps_scores ORDER BY coinsHigh DESC LIMIT 10";
  $result = $conn->query($sql);
  $response = array();
  if ($result->num_rows > 0) {
      while ($row = $result->fetch_assoc()) {
          array_push($response, $row);
      }
  }
  return $response;
}
?>