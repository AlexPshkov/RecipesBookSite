import {Injectable} from '@angular/core';
import {map, Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {DomSanitizer, SafeHtml} from "@angular/platform-browser";

@Injectable({
  providedIn: 'root'
})
export class ExternalHtmlService {
  public readonly symbols: RegExp = /[\r\n%#()<>?[\\\]^`{|}]/g;

  constructor(
    private http: HttpClient,
    private sanitizer: DomSanitizer) {
  }

  public getSvg(name: string): Observable<SafeHtml> {
    return this.getHtml('assets/images/svg/' + name + '.svg')
  }

  public getSvgRaw(name: string): Observable<string> {
    return this.getText('assets/images/svg/' + name + '.svg')
  }

  public getHtml(url: string): Observable<SafeHtml> {
    return this.getText(url).pipe(
      map((value) => this.sanitizer.bypassSecurityTrustHtml(value))
    );
  }

  public getText(url: string): Observable<string> {
    return this.http.get(url, {responseType: 'text'});
  }

  /**
   * Converts <svg> to "data:image/svg"
   * @param data
   */
  public encodeSVG(data: string): string {
    data = data.replace(/'/g, `"`);

    data = data.replace(/>\s+</g, `><`);
    data = data.replace(/\s{2,}/g, ` `);
    data = data.replace(this.symbols, encodeURIComponent);

    return "data:image/svg+xml," + data;
  }
}
