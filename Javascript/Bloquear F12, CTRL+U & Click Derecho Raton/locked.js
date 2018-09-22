$(document).ready(function(){
			var disabledKeys = [123,"17,85",117,85,122];
			//Deshabilita F12
			$(document).keydown(function(e){
			if(e.keyCode==disabledKeys[0]||e.keyCode.toString()==disabledKeys[1]) {
			 return false;
			}
			//Deshabilita CTRL+U
			if(e.keyCode==disabledKeys[2]||e.keyCode.toString()==disabledKeys[3]) {
			 return false;
			}
			});
			//Deshabilita Click derecho de ratón
			$(document).mousedown(function(e){
			 return false;
			});
			$(document).on("contextmenu",function(e){
			});	
			document.oncontextmenu = function(){return false}
		});