import './ListNews'
import React, { Component } from 'react'
import MaterialTable from 'material-table';

export class ListNews extends Component {

    constructor(props) {
        super(props);

        this.state = {
            //news: [],
            columns: [
                { title: 'Id', field: 'id', hidden: true },
                { title: 'Name', field: 'name' },
                { title: 'Description', field: 'shortDescription' },
                { title: 'Category', field: 'category' },
                { title: 'Created', field: 'created' }
            ],
            data: []
        };
    }

    async componentDidMount() {
        await this.fetchNews();
    }

    async fetchNews() {
        const { id } = this.props.match.params;

        let news;

        if (!id) {
            const reponse = await fetch('/api/GetNews');
            news = await reponse.json();
        }

        const data = news.map(x => ({
            id: x.id,
            name: x.name,
            created: new Date(x.created).toLocaleString(),
            shortDescription: x.shortDescription,
            category: x.category.name
        }));

        this.setState({
            data
        });
    }

    async DeleteNews(id) {
        const response = await fetch('/api/DeleteNews', {
            //const response = await fetch(`/api/DeleteNews/${oldData.id}`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json; charset=utf-8',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify({ id })
        });

        if (response.ok) {

        }
    }

    async EditNews(id) {
        this.props.history.push(`/createeditnews/${id}`);
    }

    render() {
        return (
            <div className='list-news'>

                <MaterialTable
                    title='News'
                    columns={this.state.columns}
                    data={this.state.data}
                    actions={[
                        {
                            icon: 'edit',
                            tooltip: 'Edit News',
                            onClick: (event, rowData) => this.EditNews(rowData.id)
                        },
                        {
                            icon: 'delete',
                            tooltip: 'Delete News',
                            onClick: (event, rowData) => this.DeleteNews(rowData.id)
                        }
                    ]}
                />
            </div>
        )
    }
}

export default ListNews
