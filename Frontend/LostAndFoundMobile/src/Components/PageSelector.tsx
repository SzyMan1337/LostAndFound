import { Dispatch, SetStateAction } from 'react';
import { Pressable, StyleSheet, Text, View } from 'react-native';
import AntDesign from 'react-native-vector-icons/AntDesign';
import { dark, light, light3, primary } from './Colors';

const range = (start: number, end: number) =>
  Array.from(Array(end - start + 1).keys()).map(x => x + start);

const DOTS = 'DOTS';

const usePagination = (
  currentPage: number,
  totalPageCount: number,
  siblingCount: number = 2,
): (string | number)[] => {
  const totalPageNumbers = siblingCount + 5;

  if (totalPageNumbers >= totalPageCount) {
    return range(1, totalPageCount);
  }
  const leftSiblingIndex = Math.max(currentPage - siblingCount, 1);
  const rightSiblingIndex = Math.min(
    currentPage + siblingCount,
    totalPageCount,
  );
  const shouldShowLeftDots = leftSiblingIndex > 2;
  const shouldShowRightDots = rightSiblingIndex < totalPageCount - 2;
  const firstPageIndex = 1;
  const lastPageIndex = totalPageCount;

  if (!shouldShowLeftDots && shouldShowRightDots) {
    let leftItemCount = 3 + 2 * siblingCount;
    let leftRange = range(1, leftItemCount);

    return [...leftRange, DOTS, totalPageCount];
  }

  if (shouldShowLeftDots && !shouldShowRightDots) {
    let rightItemCount = 3 + 2 * siblingCount;
    let rightRange = range(totalPageCount - rightItemCount + 1, totalPageCount);
    return [firstPageIndex, DOTS, ...rightRange];
  }

  if (shouldShowLeftDots && shouldShowRightDots) {
    let middleRange = range(leftSiblingIndex, rightSiblingIndex);
    return [firstPageIndex, DOTS, ...middleRange, DOTS, lastPageIndex];
  }

  return [];
};

export const PageSelector = (
  currentPage: number,
  totalPageCount: number,
  onPageChange: Dispatch<SetStateAction<number>>,
) => {
  const paginationRange = usePagination(currentPage, totalPageCount);
  if (currentPage === 0 || paginationRange.length < 2) {
    return null;
  }

  return (
    <View style={styles.selectorContainer}>
      <View>
        {currentPage === 1 ? (
          <Pressable>
            <AntDesign name="left" size={25} />
          </Pressable>
        ) : (
          <Pressable onPress={() => onPageChange(currentPage - 1)}>
            <AntDesign name="left" color={primary} size={25} />
          </Pressable>
        )}
      </View>
      <View style={styles.numbersContainer}>
        {paginationRange.map(pageNumber => {
          if (pageNumber === DOTS) {
            return (
              <Text style={[styles.pageNumber, { color: primary }]}>...</Text>
            );
          }

          if (pageNumber === currentPage) {
            return (
              <Text style={[styles.pageNumber, { color: dark }]}>
                {pageNumber}
              </Text>
            );
          }

          return (
            <Pressable onPress={() => onPageChange(Number(pageNumber))}>
              <Text style={[styles.pageNumber, { color: primary }]}>
                {pageNumber}
              </Text>
            </Pressable>
          );
        })}
      </View>
      <View>
        {currentPage === totalPageCount ? (
          <Pressable>
            <AntDesign name="right" size={25} />
          </Pressable>
        ) : (
          <Pressable onPress={() => onPageChange(currentPage + 1)}>
            <AntDesign name="right" color={primary} size={25} />
          </Pressable>
        )}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  selectorContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignContent: 'center',
    alignItems: 'center',
    padding: 8,
    borderRadius: 5,
  },
  numbersContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignContent: 'center',
  },
  pageNumber: {
    fontSize: 25,
    padding: 3,
  },
});
