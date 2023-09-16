import {Component, ElementRef, EventEmitter, HostListener, Output} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR} from "@angular/forms";

@Component({
  selector: 'image-upload',
  templateUrl: './image-upload.component.html',
  styleUrls: ['./image-upload.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: ImageUploadComponent
    }]
})
export class ImageUploadComponent implements ControlValueAccessor {
  public imagePath: string = "";

  @Output()
  imageChange = new EventEmitter<File>();

  onChange = (imagePath: string) => {
  };
  onTouched = () => {
  };
  touched = false;
  disabled = false;

  constructor(
    private host: ElementRef<HTMLInputElement>) {
  }

  @HostListener('change', ['$event.target.files']) emitFiles(event: FileList) {
    const file = event && event.item(0);
    if (file) {
      this.convertFileToURL(file);
      this.imageChange.emit(file);
      this.host.nativeElement.files = null;
    }
  }

  convertFileToURL(file: File) {
    const reader = new FileReader();
    reader.onload = () => {
      this.imagePath = reader.result as string;
      this.onChange(this.imagePath);
    };
    reader.readAsDataURL(file);
  }


  writeValue(imagePath: string) {
    this.imagePath = imagePath;
  }

  registerOnChange(onChange: any) {
    this.onChange = onChange;
  }

  registerOnTouched(onTouched: any) {
    this.onTouched = onTouched;
  }

  markAsTouched() {
    if (!this.touched) {
      this.onTouched();
      this.touched = true;
    }
  }

  setDisabledState(disabled: boolean) {
    this.disabled = disabled;
  }
}
