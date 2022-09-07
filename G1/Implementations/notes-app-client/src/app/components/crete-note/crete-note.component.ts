import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { NoteModel } from 'src/app/models/note.modles';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'app-crete-note',
  templateUrl: './crete-note.component.html',
  styleUrls: ['./crete-note.component.css']
})
export class CreteNoteComponent implements OnInit {

  token: string = ""

  createNoteForm = new FormGroup({
    Text: new FormControl(),
    Color: new FormControl(),
    TagType: new FormControl(),
  })

  constructor(private _router: Router, private _noteService: NoteService) { }

  ngOnInit(): void {
    let token = localStorage.getItem("token")

    if (!token) {
      this._router.navigate(["/login"])
    }

    this.token = token ?? ""
  }

  onSubmit() {

    let text = this.createNoteForm.value.Text
    let color = this.createNoteForm.value.Color
    let tagType = this.createNoteForm.value.TagType

    let noteModel = new NoteModel(text, color, parseInt(tagType))

    this._noteService.createNote(noteModel, this.token).subscribe({
      error: err => console.warn(err.error),
      complete: () => {
        this._router.navigate(["/notes"])
      }
    })

  }

}
