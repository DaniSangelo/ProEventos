import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {

  currentPage: string = 'Perfil';
  form!: FormGroup;

  get f(): any{
    return this.form.controls;
  }

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.validation();
  }

  private validation(): void{
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password','passwordConfirmation')
    };

    this.form = this.formBuilder.group({
      firstName: ['', Validators.required] ,
      lastName: ['', Validators.required] ,
      email: ['',
        [
          Validators.required, Validators.email
        ]
      ] ,
      telefone: ['', Validators.required] ,
      descricao: ['', Validators.required] ,
      password: ['',
        [
          Validators.required,
          Validators.minLength(6)
        ]
     ] ,
      passwordConfirmation: ['',
        [
          Validators.required,
          Validators.minLength(6)
        ]
      ]
    }, formOptions)
  }

  public resetForm(event: any): void{
    event.preventDefault();
    this.form.reset();
  }

  onSubmit(): void{
    if (this.form.invalid)
      return;
  }
}
