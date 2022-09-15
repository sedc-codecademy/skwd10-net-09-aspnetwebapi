function loginUser() {
    fetch("http://localhost:5277/api/v1/users/login", {
        method: "POST",
        headers: {
            "Accept" : "text/plain",
            "Content-Type": 'application/json'
        },
        body: JSON.stringify({
            username: "tstev",
            password: "asdzxc123"
        })
    }).then(response => response.json())
    .then(res => {
        localStorage.setItem("User", JSON.stringify(res));
    });
}

loginUser();


document.getElementById("click").addEventListener('click', function(){
    const user = JSON.parse(localStorage.getItem("User"));

    if(!user){
        console.error("Error");
        return;
    }

    fetch("http://localhost:5277/api/v1/notes/get-all", {
        headers: {
            "Authorization": `Bearer ${user.token}`,
            "Accept" : "text/plain",
            "Content-Type": 'application/json'
        }
    }).then(response => response.json())
    .then(res => console.log(res));
})