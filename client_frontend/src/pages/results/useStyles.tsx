import {makeStyles} from "@material-ui/core";

const purp = '#5926EB';

export default makeStyles({
  root: {},
  rootPagination: {
    color: 'grey',
    display: 'flex',
    justifyContent: 'flex-start',
    width: '100%',
    '& label': {
      color: 'grey',
      transform: 'translate(0px, 20%)',
    },
    '& button.Mui-selected': {
      border: `1px solid #B496FE`,
      background: '#fff',
    },
    '& nav': {
      margin: '0 15px',
    },
  },
  jumpToInput: {
    width: '50px',
    '& > div': {height: '100%'}
  },

  rowsPerPageSelect: {
    '& > div': {
      paddingTop: '7px',
      paddingBottom: '7px',
    }
  },
  menuItem: {},

  dataGrid: {
    flexDirection: 'column-reverse!important' as 'column-reverse',
    '&.MuiDataGrid-root': {
      border: 'none',
      boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    },
    '& .MuiDataGrid-footer': {
      padding: '20px 50px',
      boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    },
  },

  searchWrapper: {
    flexGrow: 1,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-around',
    padding: '30px',
    '& > span': {
      color: purp,
      marginBottom: '20px',
    },
    '& > h5': {
      font: 'Poppins',
      fontWeight : 'bold',
      marginBottom: '20px',
    }
  },
  content: {
    display: 'flex',
    flexDirection: 'column',
    background: '#fff',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  }
});



