import {makeStyles} from "@material-ui/core";

export const paleGrey = 'rgba(8, 0, 55, .4)';
export const fontColorSegment = 'rgba(8, 0, 55, .4)';
export const good = 'rgb(163,229,178)';
export const average = 'rgb(255,234,160)';
export const poor = 'rgb(255,210,210)';
export const setBarWidth = (value: number): string => `${value}%`;
export const paintRating = (value: number): string => value <= 30 ? poor :
  (value > 30 && value <= 60) ? average : good;

export default makeStyles({
  ratingListBar: {
    height: '4px',
    borderRadius: '10px',
    width: '100%',
    background: 'red',
    alignSelf: 'center',
  },
  flex: {
    display: 'flex',
  },
  padding10: {
    padding: '10px',
  },
  paddingH: {padding: '15px 0px',},
  gap5: {gap: '5px'},
  alignContentCenter: {
    alignContent: 'center',
  },
  alignItemsCenter: {
    alignItems: 'center',
  },
  researchContainer: {
    textAlign: 'center',
    flexGrow: 1,
    background: 'rgba(204, 204, 204, .5)',
    padding: '20px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  noDataText: {
    fontSize: '.9em',
    color: paleGrey,
  },
});
