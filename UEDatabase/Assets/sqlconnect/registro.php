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
	$namecheckquery = "SELECT username FROM player WHERE username = '" . $username . "';";

	$namecheck = mysqli_query($con, $namecheckquery) or die("2: error de verificación de nombre"); //error code #2 : name check query failed

	if (mysqli_num_rows($namecheck)>0)
	{
		echo "3: Nombre ya existe"; // error code #3 : nombre de usuario ya existe
		exit();

	}

	//agregar el usuario a la tabla
	$insertuserquery = "INSERT INTO player (username, password) VALUES ('" . $username . "', '"  . $password . "')";
	mysqli_query($con, $insertuserquery) or die("4: Insert player error"); // error code #4 :  error al insertar el usuario

	echo "0";

?>