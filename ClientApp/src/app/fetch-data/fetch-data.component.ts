import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public categories: Category[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Category[]>(baseUrl + 'api/Category').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
  }
}

interface Category {
  categoryid: number;
  categoryname: string;
}
