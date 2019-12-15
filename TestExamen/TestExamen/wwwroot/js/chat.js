let realtime = {}; //namespace

realtime.hub = (() => {

    //1. host address (in config file plaatsen)!
    let socketColor; //bewaren  binnen de namespace
    let username;

    //2. Socket connectie
    let connection =
        new signalR.HubConnectionBuilder()
            //.AddMessagePackProtocol()
            .withUrl("/chatHub").build();

    //nodig indien niet in de connection state
    connection.start().catch(function (err) {
        return console.error(err.toString());
    })

    //3. Hub methodes (connection.on ...):
    connection.on("ServerMessage", function (jsonMsg) {
        var messagesList = document.getElementById("messagesList");
        realtime.hub.socketColor = realtime.hub.socketColor ? realtime.hub.socketColor : jsonMsg.color; //eigenschap toevoegen aan socket 
        var msg = jsonMsg.message.replace(/&/g, "&amp;")
            .replace(/</g, "&lt;").replace(/>/g, "&gt;");

        var li = document.createElement("li");
        li.style.color = jsonMsg.color;
        li.textContent = unescape(msg);
        //messagesList.appendChild(li);
        messagesList.insertBefore(li, messagesList.childNodes[0]);
    });

    connection.on('UserImage', function (base64Image) {
        var imgElement = document.createElement("img");
        if (base64Image.imageBinary) {
            console.log("base 64 bytes");
            imgElement.src = base64Image.imageHeaders + base64Image.imageBinary;
        } else {
            console.log("base 64 string");
            imgElement.src = base64Image;
        }

        imgElement.width = 75; // zonder px    
        var div = document.createElement("div");
        div.style.position = "relative";
        div.style.display = "block";
        div.style.margin = 10 + "px";
        div.appendChild(imgElement);

        document.getElementById("messagesList").insertBefore(div, messagesList.childNodes[0]);

    });

    //Hub events 
    connection.on('Login', () => {
        realtime.hub.username = prompt('Kies een gebruikersnaam');
        connection.invoke("Login", realtime.hub.username);
    });

    //4. opstarten van HTML en javascript handlers
    let start = document.addEventListener("DOMContentLoaded", (event) => {
        console.log("DOM fully loaded and parsed");
        addHandlers();
    });

    let addHandlers = () => {
        var sendButton = document.getElementById("sendButton");

        //message event
        sendButton.addEventListener("click", (event) => {
            var messageInput = document.getElementById("messageInput");
            messageInput = escape(messageInput.value);
            event.preventDefault();

            realtime.hub.connection.invoke("ClientMessage", {
                message: messageInput,
                user: "" || realtime.hub.username,
                color: realtime.hub.socketColor
            })
                .catch(function (err) {
                    return console.error(err.toString());
                });
        });



        //image event
        if (!!document.getElementById("submitUploadButton") === false) {

            document.getElementById("selectImg").addEventListener("change", (event) => {
                for (let i = 0; i < event.currentTarget.files.length; i++) {
                    let file = event.currentTarget.files[i];  //een object "File" met oa de naam 
                    let reader = new FileReader();
                    reader.onloadend = (evt) => {
                        let result = reader.result;
                        fetch("/api/FileUpload/UploadFileByJS",
                            {
                                method: "POST",
                                body: JSON.stringify({ "formFile": result, "fileName": file.name }),//wordt base64
                                headers: {
                                    'content-type': 'application/json'
                                }
                            })
                            .then((response) => console.log(response)); //Accepted return 
                    };
                    reader.readAsDataURL(file); //leest in als base 64
                }
            });

        }
    };




    //5. publieke elementen returnen (module pattern)
    return {
        start: start,
        connection  //publiek bekend maken
    };
})();

realtime.hub.start; //de oproep is not a function -> niet start()!