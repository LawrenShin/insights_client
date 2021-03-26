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
    // flexDirection: 'column-reverse!important' as 'column-reverse',
    '&.MuiDataGrid-root': {
      border: 'none',
      boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    },
    '&.MuiDataGrid-root > div:nth-child(1)': {
      order: 1,
      display: 'flex',
      justifyContent: 'flex-end',
    },
    '&.MuiDataGrid-root > div:nth-child(2)': {
      order: 2,
    },
    '& .MuiDataGrid-footer': {
      padding: '20px 50px',
      boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    },
    '& .MuiDataGrid-main': {
      margin: '0px 50px 50px 50px',
    },
    '& .MuiDataGrid-iconSeparator': {
      display: 'none',
    },
    '& .MuiDataGrid-columnsContainer': {
      color: 'rgba(8, 0, 55, .4)',
      font: '400 13px Poppins',
    },
    '& .MuiDataGrid-row': {
      color: 'rgba(8, 0, 55, .7)',
      font: '400 14px Poppins',
      '& div:nth-child(2)': {
        fontWeight: 'bold',
        '& a': {textDecoration: 'none'},
      }
    },
    '& .MuiDataGrid-colCellTitle': {
      textDecoration: 'underline',
      fontWeight: 'bold',
      fontFamily: 'Poppins, sans-serif',
    }
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
    height: '70vh',
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    background: '#fff',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  }
});



