import {Pipe, PipeTransform} from '@angular/core';
import {imagesUrl} from "../app.component";

@Pipe({
  name: 'imageUrl'
})
export class ImageUrlPipe implements PipeTransform {

  transform(value: string | null | undefined): string {
    if (value == null || value?.length == 0) return "none";
    if (value.startsWith("data:image")) return "url('" + value + "')";
    return "url('" + imagesUrl + value + "')";
  }

}
