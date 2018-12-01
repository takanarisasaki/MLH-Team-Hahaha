function postRequestCallback(path, args, callback){
	var xhr = new XMLHttpRequest();
	var url = path;
	xhr.open("POST", url , true);
	xhr.setRequestHeader("Content-type", "application/json")
	xhr.onreadystatechange = function () {
	if (xhr.readyState === 4 && xhr.status === 200) {
	
	    var json = JSON.parse(xhr.responseText);
			callback(json)
	}
	};
	if(args){
		var data = JSON.stringify(args);
		xhr.send(data);
	}
	else{
		xhr.send()
	}
}