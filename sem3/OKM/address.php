<?php
//    error_reporting( E_ALL );
// ini_set('display_errors', 1);
?>

<html>
     <head>
         <title>Address Book</title>
     </head>
     <body>
 <?php
// Connects to your Database 
$mysqli = new mysqli("127.0.0.1", "root", "vampiryuga", "address");

if (mysqli_connect_errno()) {
    printf("Connect failed: %s\n", mysqli_connect_error());
    exit();
}

// echo "MySQL connection success!" . PHP_EOL;

$mysqli->select_db("address");

if ($result = $mysqli->query("SELECT DATABASE()")) {
    $row = $result->fetch_row();
    printf("Default database is %s.\n", $row[0]);
    $result->close();
}

// or die(mysqli_connect_error()()); 
$mode  = $_GET['mode'];
$name  = $_GET['name'];
$phone = $_GET['phone'];
$email = $_GET['email'];
$id    = $_GET['id'];
$self  = $_SERVER['PHP_SELF'];
if ($mode == "add") {
    Print '<h2>Add Contact</h2>
 <p> 
 <form action=';
    echo $self;
    Print '
 method=GET> 
 <table>
 <tr><td>Name:</td><td><input type="text" name="name" /></td></tr> 
 <tr><td>Phone:</td><td><input type="text" name="phone" /></td></tr> 
 <tr><td>Email:</td><td><input type="text" name="email" /></td></tr> 
 <tr><td colspan="2" align="center"><input type="submit" /></td></tr> 
 <input type=hidden name=mode value=added>
 </table> 
 </form> <p>';
}

if ($mode == "added") {
    mysqli_query($mysqli, "INSERT INTO address (name, phone, email) VALUES ('$name', '$phone', '$email')");
}
if ($mode == "edit") {
    Print '<h2>Edit Contact</h2> 
 <p> 
 <form action=';
    echo $self;
    Print '
 method=GET> 
 <table> 
 <tr><td>Name:</td><td><input type="text" value="';
    Print $name;
    print '" name="name" /></td></tr> 
 <tr><td>Phone:</td><td><input type="text" value="';
    Print $phone;
    print '" name="phone" /></td></tr> 
 <tr><td>Email:</td><td><input type="text" value="';
    Print $email;
    print '" name="email" /></td></tr> 
 <tr><td colspan="2" align="center"><input type="submit" /></td></tr> 
 <input type=hidden name=mode value=edited> 
 <input type=hidden name=id value=';
    Print $id;
    print '> 
 </table> 
 </form> <p>';
}

if ($mode == "edited") {
    mysqli_query($mysqli, "UPDATE address SET name = '$name', phone = '$phone', email = '$email' WHERE id = $id");
    Print "Data Updated!<p>";
}
if ($mode == "remove") {
    mysqli_query($mysqli, "DELETE FROM address where id=$id");
    Print "Entry has been removed <p>";
}

$data = mysqli_query($mysqli, "SELECT * FROM address ORDER BY name ASC");
if (mysqli_connect_errno()) {
    printf("Connect failed: %s\n", mysqli_connect_error());
    exit();
}
Print "<h2>Address Book</h2><p>\n";
Print "<table border cellpadding=3>\n";
Print "<tr><th width=100>Name</th><th width=100>Phone</th><th width=200>Email</th><th width=100 colspan=2>Admin</th></tr>\n";
Print "<td colspan=5 align=right><a href=" . $_SERVER['PHP_SELF'] . "?mode=add>Add Contact</a></td>\n";
while ($info = mysqli_fetch_array($data)) {
    Print "<tr><td>" . $info['name'] . "</td> \n";
    Print "<td>" . $info['phone'] . "</td> \n";
    Print "<td> <a href=mailto:" . $info['email'] . ">" . $info['email'] . "</a></td>\n";
    Print "<td><a href=" . $_SERVER['PHP_SELF'] . "?id=" . $info['id'] . "&name=" . $info['name'] . "&phone=" . $info['phone'] . "&email=" . $info['email'] . "&mode=edit>Edit</a></td>\n";
    Print "<td><a href=" . $_SERVER['PHP_SELF'] . "?id=" . $info['id'] . "&mode=remove>Remove</a></td></tr>\n";
}
Print "</table>\n";
?> 

<?php

// CREATE TABLE address (id INT(4) NOT NULL AUTO_INCREMENT PRIMARY KEY, name VARCHAR(30), phone VARCHAR(30), email VARCHAR(30));
// INSERT INTO address (name, phone, email) VALUES ( "Alexa", "430-555-2252", "sunshine@fakeaddress.com"), ( "Devie", "658-555-5985", "potato@monkey.us" )
?>

<p>


     </body> 
</html> 