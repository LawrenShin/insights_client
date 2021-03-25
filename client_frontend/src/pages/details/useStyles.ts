import {makeStyles} from "@material-ui/core";

const purp = '#5926EB';
const purpBack = 'rgba(89, 38, 235, .2)';

export default makeStyles({
  root: {},
  content: {
    display: 'flex',
    margin: '10vh',
    gap: '3vw',
  },
  list: {
    background: 'white',
    width: '20vw',
    color: purp,
    borderRadius: '5px',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    font: 'Poppins',
    '& ul': {
      listStyle: 'none',
      padding: '0',
    },
    '& li': {
      padding: '5px 10px',
    },
  },
  listSelected: {
    background: purpBack,
  },
  graphs: {},
});
