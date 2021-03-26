import {makeStyles} from "@material-ui/core";

const purp = '#5926EB';
const paleGrey = 'rgba(8, 0, 55, .4)';
const purpBack = 'rgba(89, 38, 235, .2)';

export default makeStyles({
  root: {},
  content: {
    display: 'flex',
    margin: '10vh',
    gap: '3vw',
  },
  contentTitle: {
    marginBottom: '20px',
  },
  list: {
    width: '20vw',
    color: purp,
    borderRadius: '5px',
    fontFamily: 'Poppins, sans-serif',
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
  purpColor: {
    color: purp,
  },
  companyDetails: {},
  companyHeader: {
    flexGrow: 1,
    '& > div': {
      gap: '3em',
      fontSize: '0.9em',
      '& > span': {lineHeight: '1.8em'},
      '& > div span:nth-child(1)': {color: paleGrey},
    }
  },
//  basics
  width100: { width: '100%'},
  paintContainer: {
    background: 'white',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  },
  titleFont: {
    fontFamily: 'Poppins, sans-serif',
    fontWeight: 'bold',
    fontSize: '1.3em',
  }
});
