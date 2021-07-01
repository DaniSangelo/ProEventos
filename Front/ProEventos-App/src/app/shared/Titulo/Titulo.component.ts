import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './Titulo.component.html',
  styleUrls: ['./Titulo.component.scss']
})

export class TituloComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  @Input() public tituloPagina: string = 'Aqui deve ser exibido o título da página'
  @Input() public subtitulo: string = 'Desde 2021';
  @Input() public iconClass: string = 'fa fa-user';
  @Input() public botaoListar: boolean = false;

  listar(): void {
    this.router.navigate([`/${this.tituloPagina.toLowerCase()}/lista`])
  }

}
