import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'app-get-notes',
  templateUrl: './get-notes.component.html',
  styleUrls: ['./get-notes.component.css']
})
export class GetNotesComponent implements OnInit {

  token: string = ""
  notes: any = []

  constructor(private _noteService: NoteService,
              private _router: Router) { }

  ngOnInit(): void {
    let token = localStorage.getItem("token")

    if (!token) {
      this._router.navigate(["/login"])
    }

    this.token = token ?? ""

    this.getNotes()
  }


  getNotes() {
    this._noteService.getNotes().subscribe({
      next: data => {
        console.log(data)
        this.notes = data
      },
      error: err => console.warn(err.error)
    })
  }

  refreshNotes() {
    this.getNotes()
  }

}
