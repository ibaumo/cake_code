<?php
///////Configuración/////
$mail_destinatario = 'miemail@dominio.com';
///////Fin configuración//

///// Funciones necesarias////
function form_mail($sPara, $sAsunto, $sTexto, $sDe)
{
$bHayFicheros = 0;
$sCabeceraTexto = "";
$sAdjuntos = "";
if ($sDe)$sCabeceras = "From:".$sDe."n";
else $sCabeceras = "";
$sCabeceras .= "MIME-version: 1.0n";
foreach ($_POST as $sNombre => $sValor)
$sTexto = $sTexto."n".$sNombre." = ".$sValor;
foreach ($_FILES as $vAdjunto)
{
if ($bHayFicheros == 0)
{
$bHayFicheros = 1;
$sCabeceras .= "Content-type: multipart/mixed;";
$sCabeceras .= "boundary="--_Separador-de-mensajes_--"n";
$sCabeceraTexto = "----_Separador-de-mensajes_--n";
$sCabeceraTexto .= "Content-type: text/plain;charset=iso-8859-1n";
$sCabeceraTexto .= "Content-transfer-encoding: 7BITn";
$sTexto = $sCabeceraTexto.$sTexto;
}
if ($vAdjunto["size"] > 0)
{
$sAdjuntos .= "nn----_Separador-de-mensajes_--n";
$sAdjuntos .= "Content-type: ".$vAdjunto["type"].";name="".$vAdjunto["name"].""n";;
$sAdjuntos .= "Content-Transfer-Encoding: BASE64n";
$sAdjuntos .= "Content-disposition: attachment;filename="".$vAdjunto["name"].""nn";
$oFichero = fopen($vAdjunto["tmp_name"], 'r');
$sContenido = fread($oFichero, filesize($vAdjunto["tmp_name"]));
$sAdjuntos .= chunk_split(base64_encode($sContenido));
fclose($oFichero);
}
}
if ($bHayFicheros)
$sTexto .= $sAdjuntos."nn----_Separador-de-mensajes_----n";
return(mail($sPara, $sAsunto, $sTexto, $sCabeceras));
}

if (isset ($_POST['enviar'])) {
if (form_mail($mail_destinatario, $_POST['asunto'],
"Los datos introducidos en el formulario son:nn", $_POST['email']))
echo '

Su mensaje a sido enviado correctamente. Gracias por contactar con nosostros

';
else echo '

Error al enviar el formulario. Por favor, inténtelo de nuevo mas tarde.

'; }

echo '
<form id="formulario" action="?" enctype="multipart/form-data" method="post">
<label for="nombre">Nombre y apellidos : </label>
<input maxlength="80" name="nombre" size="50" type="text">

<label for="email">Email : </label>
<input maxlength="60" name="email" size="50" type="text">

<label for="asunto">Asunto : </label>
<input maxlength="60" name="asunto" size="50" type="text">

<label for="mensaje">Mensaje : </label> <textarea cols="31" rows="5" name="mensaje"></textarea> 

<label for="archivo">Adjuntar archivo:
<input id="archivo" name="archivo" type="file">

</label><label for="enviar">
<input name="enviar" type="submit" value="Enviar consulta"></label>
</form>

 

';

?>