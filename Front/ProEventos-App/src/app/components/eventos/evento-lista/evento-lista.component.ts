import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss'],
  providers: [EventoService]
})
export class EventoListaComponent implements OnInit {

  modalRef: BsModalRef | any;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  larguraImg: number = 100;
  margemImg: number = 2;
  exibirImagem: boolean = true;
  private _filtroLista: string = '';

  public get filtroLista(): string
  {
    return this._filtroLista;
  }

  public set filtroLista(value: string)
  {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  public filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService,
              private router: Router) { }

  public ngOnInit() {
    this.getEventos();
    this.exibirEsconderImagem();
    this.spinner.show();
  }

  public getEventos(): void
  {
    this.eventoService.getEvento().subscribe({
        next: (_eventos: Evento[]) => {
        this.eventos = _eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: (error: any) => {
        this.spinner.hide()
        this.toastr.error('Erro ao carregar os eventos', 'Erro')
      },
      complete: () => this.spinner.hide()
    });
  }

  public exibirEsconderImagem(): void
  {
    this.exibirImagem = !this.exibirImagem;
  }

  openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastr.success('Operação realizada com sucesso','Registro removido');
  }

  decline(): void {
    this.modalRef.hide();
  }

  currentPage = 'Eventos';

  detalheEvento(id: number): void {
    this.router.navigate([`eventos/detalhe/${id}`])
  }
}
