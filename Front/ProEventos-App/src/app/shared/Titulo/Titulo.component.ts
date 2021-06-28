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

}
