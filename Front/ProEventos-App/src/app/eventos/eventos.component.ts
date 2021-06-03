import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  larguraImg: number = 50;
  margemImg: number = 2;
  exibirImagem: boolean = true;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
    this.exibirEsconderImagem();
  }

  public getEventos(): any
  {
    this.http.get('https://localhost:5001/api/evento').subscribe(
      response => this.eventos = response,
      error => console.log(error)
    );
  }

  public exibirEsconderImagem(): void
  {
    this.exibirImagem = !this.exibirImagem;
  }
}
