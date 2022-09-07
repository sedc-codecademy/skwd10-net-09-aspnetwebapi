export class NoteModel {
    constructor(text: string, color: string, tag: number) {
        this.Text = text
        this.Color = color
        this.Tag = tag
    }

    Text: string
    Color: string
    Tag: number
}