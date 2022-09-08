import { Injectable } from "@angular/core";
import { LoginModel, RegisterModel } from "../models/auth.models";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Router } from "@angular/router";

@Injectable()
export class AuthService {

    constructor(private _httpClient: HttpClient, 
                private _router: Router) {}

    register(model: RegisterModel) : Observable<any> {
        let url = `${environment.apiServerBaseUrl}api/user/register`;
        return this._httpClient.post(url, model);
    }

    login(model: LoginModel) : Observable<any> {
        let url = `${environment.apiServerBaseUrl}api/user/authenticate`;
        return this._httpClient.post(url, model)
    }

    logout() {
        localStorage.clear();
        this._router.navigate(["/login"])
    }

}