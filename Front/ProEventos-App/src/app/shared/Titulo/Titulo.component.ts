import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './Titulo.component.html',
  styleUrls: ['./Titulo.component.scss']
})

export class TituloComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  @Input() public tituloPagina: string = 'Aqui deve ser exibido o título da página'
  @Input() public subtitulo: string = 'Desde 2021';
  @Input() public iconClass: string = 'fa fa-user';
  @Input() public botaoListar: boolean = false;

}
