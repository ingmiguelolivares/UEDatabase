<?php
	$con = mysqli_connect('localhost','admin','admin','unity');

	//verificar la conección

	if (mysqli_connect_errno())
	{
		echo "1: error de conexión"; //error code #1 = connección fallida
		exit();

	}

	$username = $_POST["username"];
	$level = $_POST["level"];
	$score = $_POST["score"];

	/*//verificar si el nombre ya existe
	$namecheckquery = "SELECT username FROM player WHERE username = '" . $username . "';";
	
	$namecheck = mysqli_query($con, $namecheckquery) or die("2: error de verificación de nombre"); //error code #2 : name check query failed
	
	if (mysqli_num_rows($namecheck)!= 1)
	{
		echo "5: Usuario no existe o existe repetido"; // error code #5 : usuario no existe o esta repetido
		exit();
	}*/
	//obtener información del login de el query

	$insertuserquery = "UPDATE player SET score = " . $score . " WHERE username = '" . $username . "';";
	
	mysqli_query($con, $insertuserquery) or die("7: Insert data error"); // error code #7 :  error al insertar el usuario
	

	echo "0";
?>