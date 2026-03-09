import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox'
import { SignInDTO } from '../../models/SignInDTO';

@Component({
  selector: 'app-authentication',
  imports: [
    MatInputModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './authentication.html',
  styleUrl: './authentication.scss',
})
export class AuthenticationComponent implements OnInit  {

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef

  ) {



  }

  loginForm: FormGroup = new FormGroup({
                                        email: new FormControl('', [Validators.required, Validators.email]),
                                        password: new FormControl('', [Validators.required]),
                                        rememberMe: new FormControl('')
                                      });
  loading = false;
  error = '';

  ngOnInit(): void {

  }

  onSubmit(): void {
    if (this.loginForm.invalid) { return; }
    this.loading = true;
    const { email, password, rememberMe } = this.loginForm.value;

    let si = new SignInDTO();
    si.email = email;
    si.password = password;
    si.rememberMe = rememberMe == true;

    this.authService.SignIn(si).subscribe({
      next: () => this.router.navigate(['/']),
      error: () => {
        this.loading = false;
        this.error = 'Не удалось войти';
        this.cdr.markForCheck();

      }
    });
  }



}
