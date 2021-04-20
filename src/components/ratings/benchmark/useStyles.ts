import {makeStyles} from "@material-ui/core";
import {average, dialogPeerShadow, good, paleFont, poor} from "../../colorConstants";

export const useStyles = makeStyles({
  containerRow: {
    display: 'flex',
    flexDirection: 'row',
  },
  containerCol: {
    display: 'flex',
    flexDirection: 'column',
  },
  gap5: {gap: '5px'},
  indicator: {
    width: 'fit-content',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'flex-end',
  },
  progressBar: {},
  barParts: {
    display: 'flex',
    flexDirection: 'row',
    flexWrap: 'nowrap',
    '& div:nth-child(1)': {flexBasis: '35%'},
    '& div:nth-child(2)': {flexBasis: '32.5%'},
    '& div:nth-child(3)': {flexBasis: '32.5%'},
  },
  part: {
    display: 'flex',
    flexDirection: 'row',
    justifyContent: 'space-around',
    height: '5px',
    '& span': {
      marginTop: '10px',
    }
  },
  partC: {background: poor},
  partB: {background: average},
  partA: {background: good},
  ratingNames: {},
  dialog: {
    width: 'fit-content',
    margin: 0,
    background: '#fff',
    padding: '5px',
    borderRadius: '5px',
    boxShadow: dialogPeerShadow,
    position: 'relative',
    marginBottom: '15px',
  },
  dialogTriangle: {
    position: 'absolute',
    height: '10px',
    width: '10px',
    background: '#fff',
    left: '50%',
    marginLeft: '-5px',
    transform: 'rotate(45deg)',
  },
  triangle: {
    width: 'fit-content',
    height: '20px',
  },
  ...paleFont,
});