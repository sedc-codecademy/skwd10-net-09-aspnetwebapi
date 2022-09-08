import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NoteModel } from '../models/note.modles';

@Injectable()
export class NoteService {

  constructor(private _httpClient: HttpClient) { }

  getNotes(token: string) : Observable<any> {
    let url = `${environment.apiServerBaseUrl}api/note/getallbyuser`;
    let headers = this.createHeaders(token)

    return this._httpClient.get(url, { headers });
  }

  deleteNotes(id: number, token: string) {
    let url = `${environment.apiServerBaseUrl}api/note/deletebyid/${id}`;
    let headers = this.createHeaders(token)

    return this._httpClient.delete(url, { headers })
  }

  createNote(model: NoteModel, token: string) {
    let url = `${environment.apiServerBaseUrl}api/note/create`;
    let headers = this.createHeaders(token)

    return this._httpClient.post(url, model, { headers })
  }


  private createHeaders(token: string) : HttpHeaders {
    return new HttpHeaders({
      'Authorization' : `Bearer ${token}`
    })
  }
}
