import { Component, OnInit } from '@angular/core';
import {TituloComponent} from '../shared/Titulo/Titulo.component'
@Component({
  selector: 'app-palestrantes',
  templateUrl: './palestrantes.component.html',
  styleUrls: ['./palestrantes.component.scss']
})
export class PalestrantesComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  currentPage = 'Palestrantes'

}
