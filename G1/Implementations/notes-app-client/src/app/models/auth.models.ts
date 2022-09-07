export class RegisterModel {
    constructor(firstName: string,
                lastName: string,
                username: string,
                password: string,
                confirmPassword: string) {
        this.FirstName = firstName
        this.LastName = lastName
        this.Username = username
        this.Password = password
        this.ConfirmPassword = confirmPassword
    }

    FirstName: string
    LastName: string
    Username: string
    Password: string
    ConfirmPassword: string
}

export class LoginModel{
    constructor(username: string,
                password: string) {
        this.Username = username
        this.Password = password
    }

    Username: string
    Password: string 
}