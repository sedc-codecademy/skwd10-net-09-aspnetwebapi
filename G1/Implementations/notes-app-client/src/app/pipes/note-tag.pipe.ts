import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: "notetag"})
export class NoteTagPipe implements PipeTransform {

    transform(value: number) {

        let convertedValue = ""
        
        switch(value) {
            case 1: 
                convertedValue = "Work"
                break;
            case 2: 
                convertedValue = "Education"
                break;
            case 3: 
                convertedValue = "Home"
                break;
            case 4: 
                convertedValue = "Misc"
                break;
            case 5: 
                convertedValue = "Other"
                break;
            default: 
                convertedValue = "not in the enum"
        }

        return convertedValue;

    }

}