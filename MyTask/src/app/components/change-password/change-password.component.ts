import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLinkWithHref } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ChangePassDTO } from '../../models/ChangePassDTO';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss'],
  imports: [
    CommonModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    RouterLinkWithHref
  ]
})
export class ChangePasswordComponent implements OnInit {

  constructor(

    private authService: AuthService,
    private cdr: ChangeDetectorRef

  ) {



  }

  passFormControl = new FormControl('', [Validators.required, Validators.minLength(4)]);
  newFormControl = new FormControl('', [Validators.required, Validators.minLength(8)]);
  confirmFormControl = new FormControl('', [Validators.required, Validators.minLength(8)]);

  showResultMessage: boolean = false;
  isError: boolean = false;
  message: string = "";

  ngOnInit() {

  }

  onSubmit(): void {

    this.showResultMessage = false;

    let cp = new ChangePassDTO();

    cp.oldPass = this.passFormControl.value!;
    cp.newPass = this.newFormControl.value!;

    this.authService.ChangePass(cp).subscribe(
      date=>{
        this.isError = false;

        this.passFormControl.setValue("");
        this.newFormControl.setValue("");
        this.confirmFormControl.setValue("");

        this.message = "Пароль успешно изменен";

        this.showResultMessage = true;
        this.cdr.detectChanges();
      },
      error=>{
        this.isError = true;

        this.message = "При смене пароля произошла ошибка: ";
        this.message += error.error;

        this.showResultMessage = true;

        this.cdr.detectChanges();

        console.warn(error);
      }
    );



  }

}
