<?php
	$con = mysqli_connect('localhost','admin','admin','unity');

	//verificar la conección

	if (mysqli_connect_errno())
	{
		echo "1: error de connección"; //error code #1 = connección fallida
		exit();

	}

	$username = $_POST["username"];
	$password = $_POST["password"];

	//verificar si el nombre ya existe
	$namecheckquery = "SELECT username, password, level, score FROM player WHERE username = '" . $username . "';";
	//$namecheckquery = "SELECT usernameFROM player WHERE username = '" . $username . "';";
	$namecheck = mysqli_query($con, $namecheckquery) or die("2: error de verificación de nombre"); //error code #2 : name check query failed
	
	if (mysqli_num_rows($namecheck)!= 1)
	{
		echo "5: Usuario no existe o existe repetido"; // error code #5 : usuario no existe o esta repetido
	}
	//obtener información del login de el query
	
	$inf = mysqli_fetch_assoc($namecheck);
	$pw  = $inf["password"];


	if ($password != $pw)
	{
		echo "6: clave incorrecta";
		exit();
	}

	echo "0\t" . $inf["level"] . "\t" .$inf["score"];
?>

