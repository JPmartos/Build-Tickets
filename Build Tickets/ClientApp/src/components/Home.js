import React, { Component } from 'react';
import Form from 'react-bootstrap/Form'
import moment from "moment";

class MyTable extends Component {

    constructor(props) {
        super(props);
        this.state = {
            tickets: [],
            selectId: 0,
            title: '',
            description: '',
            date_Ocurrence: '',
            email: '',
            priority: '',
            status: 1,
            isActive: true,
            errors: {}
        };
        this.handlePrint = this.handlePrint.bind(this);
        this.editTicket = this.editTicket.bind(this);
        this.cancelEdit = this.cancelEdit.bind(this);
        this.deleteticket = this.deleteticket.bind(this);
        this.saveTicket = this.saveTicket.bind(this);
        this.removeItem = this.removeItem.bind(this);
        this.clean = this.clean.bind(this);
        this.resolveticket = this.resolveticket.bind(this);


        fetch('api/Ticket/ListTickets')
            .then(response => response.json())
            .then(data => {

                this.setState({ tickets: data, loading: false });
            });
    }

    editTicket(ticket) {
        this.setState({
            selectId: ticket.id,
            title: ticket.title,
            description: ticket.description,
            priority: ticket.priority,
            email: ticket.email
        });
    }

    handleChange(field, e) {

        const target = e.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handlePrint(e) {
        this.setState({ priority: e.target.value });
    }

    removeItem(rowId) {
        const arrayCopy = this.state.tickets.filter((row) => row.id !== rowId);
        this.setState({ tickets: arrayCopy });
    }

    handleValidation() {
        let errors = {};
        let formIsValid = true;

        //title
        if (!this.state.title) {
            formIsValid = false;
            errors["title"] = "Preencha o campo Título.";
        }

        //title
        if (this.state.title.length <= 3) {
            formIsValid = false;
            errors["title"] = "Preencha o campo com no mínimo 4 ou mais caracteres.";

        }
        //title
        if (this.state.title.length >= 256) {
            formIsValid = false;
            errors["title"] = "Limite de caracteres atingidos.";
        }

        //description
        if (this.state.description >= 256) {
            formIsValid = false;
            errors["description"] = "Limite de caracteres atingidos.";
        }
        //email
        if (!this.state.email) {
            formIsValid = false;
            errors["email"] = "Preencha o campo Email";

        }
        //priority
        if (!this.state.priority) {
            formIsValid = false;
            errors["priority"] = "Escolha uma prioridade para seu ticket";

        }

        this.setState({ errors: errors });
        return formIsValid;
    }

    saveTicket() {

        this.state.priority = parseInt(this.state.priority)

        var ticket = {
            Id: this.state.selectId,
            title: this.state.title,
            description: this.state.description,
            email: this.state.email,
            priority: this.state.priority,
            status: this.state.status,

        };

        let Method = this.state.id != null ? 'Put' : 'Post';

        fetch('api/Ticket/saveTicket', {
            method: Method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ticket)
        }).then(response => response.json())
            .then(data => {

                var count = this.state.tickets.filter((row) => row.id == data.id);
                console.log(count.length);

                if (count == count.length) {
                    this.setState({
                        tickets: this.state.tickets.concat(data)
                    })
                }

                this.setState({
                    tickets: this.state.tickets.map(el => (el.id === this.state.selectId ? {
                        ...el,
                        title: this.state.title,
                        description: this.state.description,
                        priority: this.state.priority,
                        email: this.state.email,
                    } : el))
                });

                { this.clean() }
            });
    }

    deleteticket(ticket) {

        fetch('api/Ticket?id=' + ticket.id)
            .then(response => response.json())
            .then(status => {
                if (status.success) {

                    { this.removeItem(ticket.id) }
                }
            });
    }

    resolveticket(ticket) {

        var ticket = {
            Id: ticket.id,
            title: ticket.title,
            description: ticket.description,
            email: ticket.email,
            priority: ticket.priority,
            status: ticket.status,
            date_Ocurrence: ticket.date_Ocurrence

        };
        fetch('api/Ticket/updateStatus', {
            method: 'Post',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(ticket)
        }).then(response => response.json())
            .then(data => {

                alert(data.message);

                fetch('api/Ticket/ListTickets')
                    .then(response => response.json())
                    .then(data => {
                        this.setState({ tickets: data, loading: false });
                    });
            });
    }

    cancelEdit() {

        { this.clean() }
    }

    clean() {

        this.setState({
            selectId: 0,
            title: '',
            description: '',
            priority: '',
            email: '',
            errors: {}

        });
    }

    ticketSubmit(e) {
        e.preventDefault();
        if (this.handleValidation()) {
            { this.saveTicket() }
        } else {

        }
    }

    render() {

        let contents = this.state.loading
            ? (<p><em>Loading...</em></p>)
            :
            (
                <div id="tbTickets" className="mg-top">
                    <h3>Listagem de ticket abaixo</h3>
                    <table className='table table-striped' onSubmit={this.ticketSubmit.bind(this)}>
                        <thead>
                            <tr>
                                <th>Titulo</th>
                                <th>Descrição</th>
                                <th>Date de Ocorrência</th>
                                <th>E-mail</th>
                                <th>Prioridade</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.tickets.map(ticket =>
                                <tr key={ticket.id}>
                                    <td>{ticket.title}</td>
                                    <td>{ticket.description}</td>
                                    <td>{moment(ticket.date_Ocurrence).format("DD/MM/YYYY")}</td>
                                    <td>{ticket.email}</td>
                                    <td>{ticket.priority == 1 ? 'Alta' : ticket.priority == 2 ? 'Média' : 'Baixa'}</td>
                                    <td>{ticket.status == 1 ? 'Pendente' : 'Resolvido'} </td>

                                    <td><button disabled={ticket.isActive} onClick={() => { this.editTicket(ticket) }} className="btn btn-primary mr-3 mb-3">Editar</button> </td>
                                    <td><button onClick={() => { this.deleteticket(ticket) }} className="btn btn-danger mb-3">Excluir</button></td>
                                    <td><button disabled={ticket.isActive} onClick={() => { this.resolveticket(ticket) }} className="btn btn-success mb-3">Resolver</button></td>

                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            )
        return (
            <div>
                <div class="jumbotron jumbotron-fluid">
                    <div class="container">
                        <h1 class="display-4">Seja bem vido(a) a BuildOne</h1>
                        <p class="lead">Controle seus Tickets</p>
                    </div>
                </div>

                <h3>Adicione um novo ticket abaixo</h3>

                <form name="contactform" className="contactform" onSubmit={this.ticketSubmit.bind(this)}>
                    <div className="form-row">
                        <div className="form-group col-md-4">
                            <label>Titulo</label>
                            <input ref="title" type="text" className="form-control" id="title" name="title" onChange={this.handleChange.bind(this, "title")} value={this.state.title} />
                            <span className="bg-vl font-weight-bold">{this.state.errors["title"]}</span>
                        </div>
                        <div className="form-group col-md-4">
                            <label>Description</label>
                            <input ref="description" type="text" className="form-control" id="description" name="description" onChange={this.handleChange.bind(this, "description")} value={this.state.description} />
                            <span className="bg-vl font-weight-bold">{this.state.errors["description"]}</span>
                        </div>
                        <div className="form-group col-md-4">
                            <label>Email</label>
                            <input ref="Email" type="email" className="form-control" id="email" name="email" onChange={this.handleChange.bind(this, "email")} value={this.state.email} placeholder="name@example.com" />
                            <span className="bg-vl font-weight-bold">{this.state.errors["email"]}</span>
                        </div>
                        <Form>
                            <Form.Group controlId="exampleForm.SelectCustomSizeSm">
                                <Form.Label>Selecione a prioridade</Form.Label>
                                <Form.Control as="select" size="sm" value={this.state.priority} onChange={this.handlePrint} custom>
                                    <option value="-1">Selecione...</option>
                                    <option value={1}>ALTA</option>
                                    <option value={2}>MEDIA</option>
                                    <option value={3}>BAIXA</option>
                                </Form.Control>
                            </Form.Group>
                            <span className="bg-vl font-weight-bold">{this.state.errors["priority"]}</span>
                        </Form>

                    </div>

                    <button id="submit" value="Submit" className="btn btn-primary mr-3 mb-5 float-right">Salvar</button>
                    <button type="button" onClick={() => this.cancelEdit()} className="btn btn-danger mb-5 mr-3 float-right">Cancelar</button>

                </form>

                {contents}
            </div>
        );
    }
}

export class Home extends Component {

    render() {
        return (
            <React.Fragment>
                <MyTable />
            </React.Fragment>
        );
    }

}

