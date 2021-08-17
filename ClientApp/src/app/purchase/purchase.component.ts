import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-purchase-component',
  templateUrl: './purchase.component.html'
})
export class PurchaseComponent {
  exchangeDto: ExchangeDto = new ExchangeDto();
  private apiRouter = 'https://localhost:44359';

  constructor(private http: HttpClient,
    private toastr: ToastrService,
    private spinnerService: NgxSpinnerService) {
  }

  purchase() {
    this.exchangeDto.amount = Number(this.exchangeDto.amount);
    this.exchangeDto.userId = Number(this.exchangeDto.userId);
    this.exchangeDto.currency = Number(this.exchangeDto.currency);

    this.spinnerService.show();

    this.http.post<any>(this.apiRouter + '/purchase/', this.exchangeDto).subscribe({
      next: data => {
        this.toastr.success("Save purchase exchange: " + Number(data.result.result).toFixed(3), "Success: ");
        this.spinnerService.hide();
      },
      error: error => {
        this.toastr.error(error.message, "Error: ");
        this.spinnerService.hide();
      }
    })
  }
}

export class ExchangeDto {
  constructor(
    public userId: number = 0,
    public currency: number = 0,
    public amount: number = 0
  ) { }
}
