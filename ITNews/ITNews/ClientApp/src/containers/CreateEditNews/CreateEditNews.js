import './CreateEditNews.scss'
import React, { Component } from 'react'
import List from '../../components/List/List';
import { Button, TextField, Snackbar, IconButton } from '@material-ui/core';
import MuiAlert from '@material-ui/lab/Alert';
import CloseIcon from '@material-ui/icons/Close';

class CreateEditNews extends Component {

    constructor(props) {
        super(props);

        this.state = {
            allTags: [],
            allCategories: [],
            news: null,

            isEdit: false,

            isAlert: false,
            severity: 'success',
            status: ''
        };
    }

    async componentDidMount() {
        await this.fetchTags();
        await this.fetchCategories();

        const { id } = this.props.match.params;
        if (!!id) {
            this.setState({
                isEdit: true
            }, async () => {
                const reponse = await fetch(`/api/GetNews/${id}`);
                const news = await reponse.json();

                this.setState({
                    news
                });
            });
        }
    }

    async fetchCategories() {
        const reponse = await fetch('/api/GetCategories');
        const allCategories = await reponse.json();

        this.setState({ allCategories });
    }

    async fetchTags() {
        const reponse = await fetch('/api/GetTags');
        const allTags = await reponse.json();

        this.setState({ allTags });
    }

    handleChange = (e) => {
        const { name, value } = e.target
        this.setState({ [name]: value });
    }

    async handleCreate() {
        const { allTags, allCategories, ...formData } = this.state;

        console.log(formData);

        const response = await fetch('/api/AddNews', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });

        if (response.ok) {
            this.setState({
                isAlert: true,
                severity: 'success',
                status: 'News successfuly created!'
            }, () => window.location.reload());
        } else {
            this.setState({
                isAlert: true,
                severity: 'error',
                status: 'Error creating news!'
            });
        }
    }

    render() {
        return (
            <div className='admin'>
                <div className='title'>
                    <h2>{this.state.isEdit ? 'Update news' : 'Add news'}</h2>
                </div>

                <div className='name'>
                    <TextField
                        fullWidth={true}
                        name='Name'
                        label='Name'
                        onChange={this.handleChange}
                    />
                </div>
                <div className='category'>
                    <List
                        name='Category'
                        items={this.state.allCategories}
                        onChange={(category) => this.handleChange({ target: { name: 'Category', value: category } })} />
                </div>
                <div className='tags'>
                    <List
                        name='Tags'
                        items={this.state.allTags}
                        onChange={(tag) => this.handleChange({ target: { name: 'Tags', value: [tag] } })} />
                </div>
                <div className='short'>
                    <TextField
                        multiline
                        fullWidth={true}
                        variant='outlined'
                        name='ShortDescription'
                        label='Short description'
                        rows='2'
                        onChange={this.handleChange}
                    />
                </div>
                <div className='description'>
                    <TextField
                        multiline
                        fullWidth={true}
                        variant='outlined'
                        name='Description'
                        label='Description'
                        rows='10'
                        onChange={this.handleChange}
                    />
                </div>

                <Button
                    variant='contained'
                    color='primary'
                    onClick={() => this.handleCreate()}>
                    {this.state.isEdit ? 'Update' : 'Create'}
                </Button>

                <Snackbar
                    anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
                    //key={`${vertical},${horizontal}`}
                    open={this.state.isAlert}
                    onClose={() => this.setState({ isAlert: false })}
                    autoHideDuration={3000}
                    action={
                        <IconButton size='small' color='inherit' onClick={() => this.setState({ isAlert: false })}>
                            <CloseIcon fontSize='small' />
                        </IconButton>
                    }>
                    <MuiAlert
                        elevation={6}
                        variant='filled'
                        severity='success'
                        onClose={() => this.setState({ isAlert: false })}>
                        {this.state.status}
                    </MuiAlert >
                </Snackbar>
            </div>
        )
    }
}

export default CreateEditNews
