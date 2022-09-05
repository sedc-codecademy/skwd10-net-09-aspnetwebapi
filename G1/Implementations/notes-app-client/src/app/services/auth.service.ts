import { Injectable } from "@angular/core";
import { RegisterModel } from "../models/auth.models";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";

@Injectable()
export class AuthService {

    constructor(private _httpClient: HttpClient) {}

    register(model: RegisterModel) : Observable<any> {
        let url = `${environment.apiServerBaseUrl}api/user/register`;
        return this._httpClient.post(url, model);
    }

}