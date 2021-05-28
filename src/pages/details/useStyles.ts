import {makeStyles} from "@material-ui/core";
import {paleGrey, main, darkBlueBack} from "../../components/colorConstants";

export default makeStyles({
  root: {
    fontFamily: 'Poppins',
  },
  generalContainer: {

  },
  link: {
    '&:hover': {
      cursor: 'pointer',
      fontWeight: 'bold',
    },
  },
  centerLoader: {
    position: 'absolute',
    top: '50%',
    left: '50%',
    margin: '-20px 0 0 -20px',
  },
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
    color: main,
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
    background: darkBlueBack,
  },
  purpColor: {
    color: main,
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
  overflowBarList: {
    maxHeight: '250px',
    overflow: 'scroll',
    flexWrap: 'nowrap',
  },
//  basics
  listStyleNone: {listStyle: 'none'},
  width100: { width: '100%'},
  gap5: {gap: '5px'},
  gap20: {gap: '20px'},
  paintContainer: {
    background: 'white',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  },
  titleFont: {
    fontFamily: 'Poppins, sans-serif',
    fontWeight: 'bold',
    fontSize: '1.3em',
  },
  paleFont: {
    fontSize: '.9em',
    color: paleGrey,
  },
  titleSubFontSize: {fontSize: '1em'},
  littleFont: {fontSize: '.8em'},
  centerChart: {
    justifyContent: 'center',
    display: 'flex',
    marginTop: '15px',
  },
  flexCol: {
    display: 'flex',
    flexDirection: 'column',
  },
  pieChartContainer: {
    '& > span': { fontSize: '.8em' },
  },
  essentialContainerSpacing: {
    maxWidth: '33%',
    padding: '10px',
  }
});
