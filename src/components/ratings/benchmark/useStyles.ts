import {makeStyles} from "@material-ui/core";
import {average, dialogPeerShadow, good, paleFont, poor, RaceColorMap} from "../../colorConstants";

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
    width: 0,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'flex-end',
    position: 'relative',
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
  triangle: {
    width: 'fit-content',
    height: '20px',
  },
  circle: {
    height: '20px',
    width: '20px',
    borderRadius: '50%',
    background: RaceColorMap.get('caucasian'),
    boxShadow: dialogPeerShadow,
  },
  mrgTop50: {marginTop: '30px'},
  localLegendItem: {
    display: 'flex',
    gap: '5px',
  },
  ...paleFont,
});