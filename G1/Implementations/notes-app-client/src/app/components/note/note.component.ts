import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {

  @Input() note: any
  @Output() emitter = new EventEmitter() 

  constructor(private _noteService: NoteService) { }

  ngOnInit(): void {}

  deleteNote() {
    this._noteService.deleteNotes(this.note.Id).subscribe({
      error: err => err.error,
      complete: () => {
        this.emitter.emit()
      }
    })
  }

}
