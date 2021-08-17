import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  currency: number;
  exchange: number;

  private apiRouter = 'https://localhost:44359';

  constructor(private http: HttpClient,
    private toastr: ToastrService,
    private spinnerService: NgxSpinnerService) {
  }

  quote() {
    this.spinnerService.show();
    this.http.get<any>(this.apiRouter + '/quote/' + this.currency).subscribe({
      next: data => {
        this.exchange = data;
        this.toastr.success("The rate exchange: " + data, "Success: ");
        this.spinnerService.hide();
      },
        error: error => {
          this.toastr.error(error.message, "Error: ");
          this.spinnerService.hide();
        }
    })
  }
}
