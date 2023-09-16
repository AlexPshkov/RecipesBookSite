import {Injectable} from '@angular/core';
import {Location} from '@angular/common'
import {NavigationEnd, Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class NavigationService {
  private history: string[] = []

  constructor(private router: Router, private location: Location) {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if (event.urlAfterRedirects == "/main-page" && this.history.length == 0) return;
        this.history.push(event.urlAfterRedirects)
      }
    })
  }

  back(): void {
    this.history.pop()
    if (!this.isEnd()) {
      this.location.back();
      return;
    }
    this.router.navigateByUrl('/').then(() => this.history = [])
  }

  reload() {
    location.reload();
  }

  isEnd(): boolean {
    return this.history.length == 0;
  }
}
